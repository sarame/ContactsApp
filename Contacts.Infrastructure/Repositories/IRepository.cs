using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Contacts.Infrastructure.Repositories
{
    public interface IRepository<T>
    {
        Task Add(T entity);
        void Update(T entity, Guid id);
        T Get(Guid id);
        List<T> All();
        void Delete(Guid id);
    }
}
