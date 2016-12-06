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
		[BsonElement("UserId")]
		public string UserId { get; set; }
		[BsonElement("Controller")]
		public string Controller { get; set; }
		[BsonElement("Action")]
		public string Action { get; set; }
		[BsonElement("Placement")]
		public int Placement { get; set; }
		[BsonRepresentation(BsonType.ObjectId)]
		[BsonElement("ArticleId")]
		public string ArticleId { get; set; }
		[BsonElement("Template")]
		public string Template { get; set; }
		[BsonElement("Level")]
		public int Level { get; set; }
		[BsonElement("Active")]
		public bool Active { get; set; }
		[BsonElement("PublishDate")]
        public DateTime PublishDate { get; set; }
		[BsonElement("ExpireDate")]
		public DateTime? ExpireDate { get; set; }
		[BsonElement("CreatedDate")]
		public DateTime CreatedDate { get; set; }
		[BsonElement("ChangedDate")]
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
