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

        public virtual async Task AddAsync(T record)
        {
            await dbCollection.InsertOneAsync(record);
        }

        public virtual async Task<T> GetAsync(Guid id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            return await dbCollection.FindAsync(filter).Result.FirstOrDefaultAsync();
        }

        public virtual async Task<List<T>> AllAsync()
        {
            var all = await dbCollection.FindAsync(Builders<T>.Filter.Empty);
            return await all.ToListAsync();
        }

        public virtual async Task UpdateAsync(T record, Guid id)
        {
            await dbCollection.ReplaceOneAsync(new BsonDocument("_id", id), record);
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            await dbCollection.DeleteOneAsync(filter);
        }
    }
}
