using Contacts.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contacts.Services
{
    public interface IContactsServices
    {
        Task AddAsync(Contact entity);
        Task UpdateAsync(Contact entity, Guid id);
        Task<Contact> GetAsync(Guid id);
        Task<List<Contact>> AllAsync();
        Task DeleteAsync(Guid id);
    }

}
