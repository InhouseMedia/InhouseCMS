namespace Api.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Mvc;
    
	using MongoDB.Bson;
	using MongoDB.Bson.Serialization.Attributes;

    public class Article
    {
        public ObjectId Id { get; set; }
        [BsonElement("UserId")]
        public ObjectId UserId { get; set; }
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
/*
    public interface IArticleContent
	{
		ObjectId Id { get; set; }
		ObjectId ArticleId { get; set; }
		int UserId { get; set; }
		string Title { get; set; }
		string Text { get; set; }
		string Code { get; set; }
        string Action { get; set; }
		int Level { get; set; }
		bool Active { get; set; }
		DateTime PublishDate { get; set; }
		DateTime? ExpireDate { get; set; }
		DateTime CreatedDate { get; set; }
	}
*/
    public class ArticleContent//: IArticleContent
	{
		public ObjectId Id { get; set; }
		public ObjectId ArticleId { get; set; }
        [BsonElement("UserId")]
		public ObjectId UserId { get; set; }
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
        public ObjectId Id { get; set; }
        [BsonElement("UserId")]
        public ObjectId UserId { get; set; }
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