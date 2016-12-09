namespace Library.Models
{
	using System;
	using System.Collections.Generic;
    
	using MongoDB.Bson;
	using MongoDB.Bson.Serialization.Attributes;

    public class Article
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set;}
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("UserId")]
        public string UserId { get; set; }
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

    public class ArticleContent
	{
        [BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }
		[BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("ArticleId")]
        public string ArticleId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("UserId")]
		public string UserId { get; set; }
        [BsonElement("Title")]
		public string Title { get; set; }
        [BsonElement("Text")]
		public string Text { get; set; }
        [BsonElement("Code")]
		public string Code { get; set; }
        [BsonElement("Action")]
	    public string Action { get; set; }
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

    public class ArticlePage
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("UserId")]
        public string UserId { get; set; }
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

        public IEnumerable<ArticleContent> ArticleContent { get; set; }
    }
}