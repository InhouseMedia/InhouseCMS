namespace api.Models
{
	using System;
	using MongoDB.Bson;
	using MongoDB.Bson.Serialization.Attributes;

    public class User
    {
        public ObjectId Id { get; set; }
        [BsonElement("UserName")]
        public string UserName { get; set; }    
        [BsonElement("Password")]
        public string Password { get; set; }
        [BsonElement("FirstName")]
        public string FirstName { get; set; }
        [BsonElement("LastName")]
        public string LastName { get; set; }
        [BsonElement("Prefix")]
        public string Prefix { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonElement("EmailConfirmed")]
        public bool EmailConfirmed { get; set; } 
        [BsonElement("PhoneNumber")]
        public string PhoneNumber { get; set; }
        [BsonElement("PhoneNumberConfirmed")]
        public bool PhoneNumberConfirmed { get; set; }
        [BsonElement("SessionId")]
        public string SessionId { get; set; }
        [BsonElement("Active")]
        public bool Active { get; set; } 
        [BsonElement("LockedDate")]
        public DateTime LockedDate { get; set; }
        [BsonElement("LastLoginDate")]
        public DateTime LastLoginDate { get; set; }
        [BsonElement("CreatedDate")]
        public DateTime CreatedDate { get; set; }
        [BsonElement("ChangedDate")]
        public DateTime ChangedDate { get; set; }
    }
}