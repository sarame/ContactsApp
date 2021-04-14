using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Contacts.Infrastructure.Repositories
{
    public abstract class GenericRepository<T> 
        : IRepository<T> where T : class
    {
        protected MongoContext context;

        public GenericRepository(MongoContext context)
        {
            this.context = context;
        }

        public virtual void Add(string table, T record)
        {
            var collection = this.context.db.GetCollection<T>(table);
            collection.InsertOne(record);
        }

        public virtual T Get(string table,Guid id)
        {
            var collection = this.context.db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);
            return collection.Find(filter).First();
        }

        public virtual List<T> All(string table)
        {
            var collection = this.context.db.GetCollection<T>(table);
            return collection.Find(new BsonDocument()).ToList();
        }

        public virtual void Update(string table,T record, Guid id)
        {
            var collection = this.context.db.GetCollection<T>(table);
            collection.ReplaceOne(new BsonDocument("_id", id), record, new ReplaceOptions { IsUpsert = true });
        }

        public virtual void Delete(string table, Guid id)
        {
            var collection = this.context.db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);
            collection.DeleteOne(filter);
        }

    }
}
