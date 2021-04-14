using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Contacts.Domain.Models
{
    public class Contact
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Phone { get; set; }
    }
}

