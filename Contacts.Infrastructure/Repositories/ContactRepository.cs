using Contacts.Domain.Models;
using Contacts.Infrastructure;
using System.Linq;

namespace Contacts.Infrastructure.Repositories
{
    public class ContactRepository : GenericRepository<Contact>
    {
        public ContactRepository(MongoContext context) : base(context)
        {  
        }
    }
}
