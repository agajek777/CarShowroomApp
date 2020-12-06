using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarShowroom.Domain.Models.Identity
{
    public class Client
    {
        [BsonId]
        [BsonElement("id")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfNull]
        public string Id { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("identityId")]
        [BsonRequired]
        public string IdentityId { get; set; }
        [BsonElement("description")]
        public string Description { get; set; }
        [BsonElement("avatar")]
        public string Avatar { get; set; }
        [BsonElement("offers")]
        public ICollection<Offer> Offers { get; set; }
    }
}
