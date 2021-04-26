using MongoDB.Driver;

namespace Contacts.Domain
{
    public interface IMongoContext
    {
        IMongoCollection<Contact> GetCollection<Contact>(string name);
    }
}