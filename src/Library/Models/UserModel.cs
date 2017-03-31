namespace Library.Models
{
	using System;
	using MongoDB.Bson;
	using MongoDB.Bson.Serialization.Attributes;

	public class User
	{
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Prefix { get; set; }
		public string Email { get; set; }
		public bool EmailConfirmed { get; set; }
		public string PhoneNumber { get; set; }
		public bool PhoneNumberConfirmed { get; set; }
		public string SessionId { get; set; }
		public bool Active { get; set; }
		public DateTime LockedDate { get; set; }
		public DateTime LastLoginDate { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime ChangedDate { get; set; }
	}
}