using Contacts.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contacts.Services
{
    public interface IContactsServices
    {
        Task Add(Contact entity);
        Task Update(Contact entity, Guid id);
        Task<Contact> Get(Guid id);
        Task<List<Contact>> All();
        Task Delete(Guid id);
    }

}
