using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Contacts.Domain.Models
{
    public class Contact
    {
        [BsonId]
        public Guid Id { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        public String Company { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
    }
}

