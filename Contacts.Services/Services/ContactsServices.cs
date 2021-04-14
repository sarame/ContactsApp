using Contacts.Domain.Models;
using Contacts.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contacts.Services
{
    public class ContactsServices
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
        public List<Contact> All()
        {
            return _repository.All();
        }

        public Contact Get(Guid id)
        {
            return _repository.Get(id);
        }

        public void Update(Contact obj, Guid id)
        {
            _repository.Update(obj, id);
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }
    }

}
