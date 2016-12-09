namespace Library.Models
{
	using System;
	using System.Collections.Generic;
    
	using MongoDB.Bson;
	using MongoDB.Bson.Serialization.Attributes;

    public class NavigationItem
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("ArticleId")]
        public string ArticleId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("UserId")]
        public string UserId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("ParentId")]
        public string ParentId { get; set; }
        [BsonElement("Level")]
        public int Level { get; set; }
        [BsonElement("Priority")]
        public double Priority { get; set; }
        [BsonElement("Title")]
        public string Title { get; set; }
        [BsonElement("Url")]
        public string Url { get; set; }
        [BsonElement("OnClick")]
        public string OnClick { get; set; }
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

    public class NavigationSitemap
	{
		public NavigationSitemap()
		{
			ChildLocations = new HashSet<NavigationSitemap>();
		}
            
		public string Title { get; set; }
		public string Url { get; set; }
		public string OnClick { get; set; } 
		public double Priority { get; set; }
		public IEnumerable<NavigationSitemap> ChildLocations { get; set; }
		
		public NavigationSitemap Clone()
		{
			return (NavigationSitemap) this.MemberwiseClone();
		}
	}
}