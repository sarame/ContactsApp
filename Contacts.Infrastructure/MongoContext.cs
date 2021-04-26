using Contacts.Domain;
using Contacts.Infrastructure.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Contacts.Infrastructure
{
    public class MongoContext : IMongoContext
    {
        public IMongoDatabase db;
        public MongoContext(IOptions<SettingsModel> appSettings)
        {
            MongoClient client = new MongoClient(appSettings.Value.ConnectionString);
            db = client.GetDatabase(appSettings.Value.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return db.GetCollection<T>(name);
        }
    }
}
