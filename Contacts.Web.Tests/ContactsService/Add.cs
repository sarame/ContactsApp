using Contacts.Domain.Models;
using Contacts.Infrastructure.Repositories;
using Contacts.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Contacts.Web.Tests.ContactsService
{
    [TestClass]
    public class Add
    {

        [TestMethod]
        public async Task AddNullContact_ThrowError()
        {
            // ARRANGE 
            var contactsRepository = new Mock<IRepository<Contact>>();
            var contactsServices = new ContactsServices(contactsRepository.Object);

            //Act & Assert 
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => contactsServices.AddAsync(null));
            contactsRepository.Verify(e => e.AddAsync(It.IsAny<Contact>()), Times.Never);
        }

        [TestMethod]
        public async Task ValidContact_InsertNewContact()
        {
            // ARRANGE 
            var contactsRepository = new Mock<IRepository<Contact>>();
            var contactsServices = new ContactsServices(contactsRepository.Object);
            var contact = new Contact { Name = "sdsdsd", Phone = "165456848", email = "s@gmail.com", Company = "inc" };
            //Act 
            await contactsServices.AddAsync(contact);
            // Assert 
            contactsRepository.Verify(e => e.AddAsync(It.IsAny<Contact>()), Times.Once);
        }
    }
}
