using Contacts.Domain.Models;

namespace Contacts.Infrastructure.Repositories
{
    public class ContactRepository : GenericRepository<Contact>
    {
        public ContactRepository(MongoContext context) : base(context)
        {
        }
    }
}
