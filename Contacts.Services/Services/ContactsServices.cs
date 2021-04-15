using Contacts.Domain.Models;
using Contacts.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contacts.Services
{
    public class ContactsServices : IContactsServices
    {
        private readonly IRepository<Contact> _repository;

        public ContactsServices(IRepository<Contact> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(Contact obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(typeof(Contact).Name + " object is null");
            }
            await _repository.AddAsync(obj);
        }
        public async Task<List<Contact>> AllAsync()
        {
            return await _repository.AllAsync();
        }

        public async Task<Contact> GetAsync(Guid id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task UpdateAsync(Contact obj, Guid id)
        {
            await _repository.UpdateAsync(obj, id);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }

}
