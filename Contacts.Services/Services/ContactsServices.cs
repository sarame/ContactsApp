using Contacts.Domain.Models;
using Contacts.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contacts.Services
{
    public class ContactsServices: IContactsServices
    {
        private readonly IRepository<Contact> _repository;

        public ContactsServices(IRepository<Contact> repository)
        {
            _repository = repository;
        }

        public async Task Add(Contact obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(typeof(Contact).Name + " object is null");
            }
            await _repository.Add(obj);
        }
        public async Task<List<Contact>> All()
        {
            return await _repository.All();
        }

        public async Task<Contact> Get(Guid id)
        {
            return await _repository.Get(id);
        }

        public async Task Update(Contact obj, Guid id)
        {
            await _repository.Update(obj, id);
        }

        public async Task Delete(Guid id)
        {
            await _repository.Delete(id);
        }
    }

}
