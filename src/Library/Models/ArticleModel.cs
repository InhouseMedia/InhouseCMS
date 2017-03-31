namespace Library.Models
{
	using System;
	using System.Collections.Generic;

	using MongoDB.Bson;
	using MongoDB.Bson.Serialization.Attributes;

	public class Article
	{
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }
		[BsonRepresentation(BsonType.ObjectId)]
		public string UserId { get; set; }
		public int CompanyId { get; set; }
		public string MetaTitle { get; set; }
		public string MetaDescription { get; set; }
		public string MetaKeywords { get; set; }
		public string Controller { get; set; }
		public string Action { get; set; }
		public string Locale { get; set; }
		public string Template { get; set; }
		public bool Active { get; set; }
		public DateTime PublishDate { get; set; }
		public DateTime? ExpireDate { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? ChangedDate { get; set; }
	}

	public class ArticleContent
	{
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }
		[BsonRepresentation(BsonType.ObjectId)]
		public string ArticleId { get; set; }
		[BsonRepresentation(BsonType.ObjectId)]
		public string UserId { get; set; }
		public string Title { get; set; }
		public string Text { get; set; }
		public string Code { get; set; }
		public string Action { get; set; }
		public int Level { get; set; }
		public bool Active { get; set; }
		public DateTime PublishDate { get; set; }
		public DateTime? ExpireDate { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? ChangedDate { get; set; }
	}

	public class ArticlePage
	{
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }
		[BsonRepresentation(BsonType.ObjectId)]
		public string UserId { get; set; }
		public int CompanyId { get; set; }
		public string MetaTitle { get; set; }
		public string MetaDescription { get; set; }
		public string MetaKeywords { get; set; }
		public string Controller { get; set; }
		public string Action { get; set; }
		public string Locale { get; set; }
		public string Template { get; set; }
		public bool Active { get; set; }
		public DateTime PublishDate { get; set; }
		public DateTime? ExpireDate { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? ChangedDate { get; set; }
		public IEnumerable<ArticleContent> ArticleContent { get; set; }
	}
}