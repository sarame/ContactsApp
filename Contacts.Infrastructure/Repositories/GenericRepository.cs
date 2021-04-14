using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Contacts.Infrastructure.Repositories
{
    public abstract class GenericRepository<T>
        : IRepository<T> where T : class
    {
        protected MongoContext context;
        protected IMongoCollection<T> dbCollection;

        public GenericRepository(MongoContext context)
        {
            this.context = context;
            this.dbCollection = context.GetCollection<T>(typeof(T).Name);
        }

        public virtual async Task Add(T record)
        {
            await dbCollection.InsertOneAsync(record);
        }

        public virtual T Get(Guid id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            return dbCollection.Find(filter).First();
        }

        public virtual List<T> All()
        {
            return dbCollection.Find(new BsonDocument()).ToList();
        }

        public virtual void Update(T record, Guid id)
        {
            dbCollection.ReplaceOne(new BsonDocument("_id", id), record);
        }

        public virtual void Delete(Guid id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            dbCollection.DeleteOne(filter);
        }
    }
}
