using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contacts.Infrastructure.Repositories
{
    public interface IRepository<T>
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity, Guid id);
        Task<T> GetAsync(Guid id);
        Task<List<T>> AllAsync();
        Task DeleteAsync(Guid id);
    }
}
