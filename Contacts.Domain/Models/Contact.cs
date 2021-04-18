using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Contacts.Domain.Models
{
    public class Contact
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public String Company { get; set; }
        public string email { get; set; }
    }
}

