using MongoDB.Driver;

namespace Contacts.Infrastructure
{
    public interface IMongoContext
    {
        IMongoCollection<Contact> GetCollection<Contact>(string name);
    }
}