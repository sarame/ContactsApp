using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
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

        public virtual async Task<T> Get(Guid id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            return await dbCollection.FindAsync(filter).Result.FirstOrDefaultAsync();
        }

        public virtual async Task<List<T>> All()
        {
            var all = await dbCollection.FindAsync(Builders<T>.Filter.Empty);
            return await all.ToListAsync();
        }

        public virtual async Task Update(T record, Guid id)
        {
            await dbCollection.ReplaceOneAsync(new BsonDocument("_id", id), record);
        }

        public virtual async Task Delete(Guid id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            await dbCollection.DeleteOneAsync(filter);
        }
    }
}
