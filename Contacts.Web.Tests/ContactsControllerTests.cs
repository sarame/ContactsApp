using Contacts.Domain.Models;
using Contacts.Infrastructure;
using Contacts.Infrastructure.Repositories;
using Contacts.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Contacts.Web.Tests
{
    [TestClass]
    public class ContactsControllerTests
    {
        [TestMethod]
        public void CanCreateOrderWithCorrectModel()
        {
            //// ARRANGE 
            var contactsRepository = new Mock<IRepository<Contact>>();
            var mockContext = new Mock<MongoContext>();
            var contactsServices = new Mock<ContactsServices>();
           // var response = contactsServices.Object.Add(null);
            //    _mockCollection.Setup(op => op.InsertOneAsync(_book, null,
            //    default(CancellationToken))).Returns(Task.CompletedTask);

            //    _mockContext.Setup(c => c.GetCollection<Book>(typeof(Book).Name)).Returns(_mockCollection.Object);
            //    var bookRepo = new BookRepository(_mockContext.Object);

            //    //Act
            //    await bookRepo.Create(_book);

            //    //Assert 

            //    //Verify if InsertOneAsync is called once 
            //    _mockCollection.Verify(c => c.InsertOneAsync(_book, null, default(CancellationToken)), Times.Once);

        }
    }
}
