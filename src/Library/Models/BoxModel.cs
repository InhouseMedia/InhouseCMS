namespace Library.Models
{
	using System;

	using MongoDB.Bson;
	using MongoDB.Bson.Serialization.Attributes;

	public class Box
	{
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }
		[BsonRepresentation(BsonType.ObjectId)]
		public string UserId { get; set; }
		public string Controller { get; set; }
		public string Action { get; set; }
		public int Placement { get; set; }
		[BsonRepresentation(BsonType.ObjectId)]
		public string ArticleId { get; set; }
		public string Template { get; set; }
		public int Level { get; set; }
		public bool Active { get; set; }
		public DateTime PublishDate { get; set; }
		public DateTime? ExpireDate { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? ChangedDate { get; set; }
	}


	public enum BoxPlacement
	{
		None,
		Header,
		Footer,
		Left,
		Right,
		Top,
		Bottom
	}

	public class BoxVisibility
	{
		public int Key { get; set; }
		public string Name { get; set; }
		public string Active { get; set; }
		public string Translation { get; set; }
	}
}
