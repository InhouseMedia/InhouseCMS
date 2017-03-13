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
		public string ArticleId { get; set; }
		[BsonRepresentation(BsonType.ObjectId)]
		public string UserId { get; set; }
		[BsonRepresentation(BsonType.ObjectId)]
		public string ParentId { get; set; }
		public int Level { get; set; }
		public double Priority { get; set; }
		public string Title { get; set; }
		public string Url { get; set; }
		public string OnClick { get; set; }
		public string Locale { get; set; }
		public bool Active { get; set; }
		public bool Visible { get; set; }
		public DateTime PublishDate { get; set; }
		public DateTime? ExpireDate { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? ChangedDate { get; set; }
	}

	public class NavigationSitemap
	{
		public NavigationSitemap()
		{
			ChildLocations = new HashSet<NavigationSitemap>();
		}
		public string ArticleId { get; set; }
		public string Title { get; set; }
		public string Url { get; set; }
		public string OnClick { get; set; }
		public double Priority { get; set; }
		public IEnumerable<NavigationSitemap> ChildLocations { get; set; }

		public NavigationSitemap Clone()
		{
			return (NavigationSitemap)this.MemberwiseClone();
		}
	}
}