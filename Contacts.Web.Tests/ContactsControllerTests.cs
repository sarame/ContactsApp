using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Contacts.Domain.Models;
using Contacts.Infrastructure.Repositories;
using Contacts.Web.Controllers;
using Microsoft.Extensions.Logging;
using System;

namespace Contacts.Web.Tests
{
    [TestClass]
    public class ContactsControllerTests
    {
        [TestMethod]
        public void CanCreateOrderWithCorrectModel()
        {
            // ARRANGE 
            var contactsRepository = new Mock<IRepository<Contact>>();
            var logger = new Mock<ILogger<ContactsController>>();

            var contactsController = new ContactsController(logger.Object,
                contactsRepository.Object
            );

            var contactRecord = new Contact { Name = "kero", Phone = 01229768949 };

            // ACT

            contactsController.InsertContact(contactRecord);

            // ASSERT

           //contactsRepository.Verify(r => r.Update("Contacts", contactRecord, new Guid("5ac6bc25-5c63-49f4-bfb6-7153ae2f866e")), 
           //    Times.AtLeastOnce());
        }
    }
}
