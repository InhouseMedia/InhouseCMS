namespace api.Models
{
	using System;
	using MongoDB.Bson;
	using MongoDB.Bson.Serialization.Attributes;

    public class Article
    {
        public ObjectId Id { get; set; }

        [BsonElement("UserId")]
        public int UserId { get; set; }

        [BsonElement("CompanyId")]
        public int CompanyId { get; set; }

        [BsonElement("MetaTitle")]
        public string MetaTitle { get; set; }

        [BsonElement("MetaDescription")]
        public string MetaDescription { get; set; }

        [BsonElement("MetaKeywords")]
        public string MetaKeywords { get; set; }

        [BsonElement("Controller")]
        public string Controller { get; set; }

        [BsonElement("Action")]
        public string Action { get; set; }

        [BsonElement("Locale")]
        public string Locale { get; set; } 

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
}