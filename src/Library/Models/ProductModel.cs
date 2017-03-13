namespace Library.Models
{
	using System;

	using MongoDB.Bson;
	using MongoDB.Bson.Serialization.Attributes;

	public class Product
	{
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }
		[BsonRepresentation(BsonType.ObjectId)]
		public string UserId { get; set; }
		public int CompanyId { get; set; }
		public string MetaTitle { get; set; }
		public string MetaDescription { get; set; }
		public string MetaKeywords { get; set; }
		public string Title { get; set; }
		public string SubTitle { get; set; }
		public string Text { get; set; }
		public string Category { get; set; }
		public Double Price { get; set; }
		public int Stock { get; set; }
		public bool Vat { get; set; }
		public string Code { get; set; }
		public string Controller { get; set; }
		public string Action { get; set; }
		public string Locale { get; set; }
		public bool Active { get; set; }
		public DateTime PublishDate { get; set; }
		public DateTime? ExpireDate { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? ChangedDate { get; set; }
	}
}