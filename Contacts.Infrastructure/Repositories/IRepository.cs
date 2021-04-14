using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Contacts.Infrastructure.Repositories
{
    public interface IRepository<T>
    {
        void Add(string table, T entity);
        void Update(string table, T entity, Guid id);
        T Get(string table, Guid id);
        List<T> All(string table);
        void Delete(string table, Guid id);
    }
}
