using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarShowroom.Domain.Models.DTO
{
    public class ClientDto
    {
        [BsonElement("userName")]
        [BsonRequired]
        public string UserName { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("identityId")]
        [BsonRequired]
        public string IdentityId { get; set; }
        [BsonElement("description")]
        public string Description { get; set; }
        [BsonElement("avatar")]
        public string Avatar { get; set; }
    }
}
