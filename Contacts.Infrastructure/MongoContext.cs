using MongoDB.Driver;

namespace Contacts.Infrastructure
{
    public class MongoContext
    {
        public IMongoDatabase db;

        public MongoContext()
        {
            MongoClient client = new MongoClient();
            db = client.GetDatabase("PhoneContacts");
        }
    }
}
