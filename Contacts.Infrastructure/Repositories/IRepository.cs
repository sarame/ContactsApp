using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Contacts.Infrastructure.Repositories
{
    public interface IRepository<T>
    {
        Task Add(T entity);
        Task Update(T entity, Guid id);
        Task<T> Get(Guid id);
        Task<List<T>> All();
        Task Delete(Guid id);
    }
}
