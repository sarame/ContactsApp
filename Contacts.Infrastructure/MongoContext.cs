using MongoDB.Driver;

namespace Contacts.Infrastructure
{
    public class MongoContext : IMongoContext
    {
        public IMongoDatabase db;

        public MongoContext()
        {
            MongoClient client = new MongoClient(DBConfigrationSettings.connectionString);
            db = client.GetDatabase(DBConfigrationSettings.DBName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return db.GetCollection<T>(name);
        }
    }
}
