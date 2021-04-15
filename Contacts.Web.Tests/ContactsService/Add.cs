using Contacts.Domain.Models;
using Contacts.Infrastructure.Repositories;
using Contacts.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

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
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => contactsServices.Add(null));
            contactsRepository.Verify(e=>e.Add(It.IsAny<Contact>()),Times.Never);
        }

        [TestMethod]
        public async Task AddCorrectContact_()
        {
            // ARRANGE 

            var contactsServices = new Mock<IContactsServices>();

            //Act & Assert 
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => contactsServices.Object.Add(null));

        }
    }
}
