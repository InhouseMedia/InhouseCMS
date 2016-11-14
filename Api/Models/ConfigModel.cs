namespace api.Config
{

	using MongoDB.Bson;
	using MongoDB.Bson.Serialization.Attributes;
	
	public class Config
	{
		public ObjectId Id { get; set; }
		[BsonElement("Domain")]
		public string Domain { get; set; }
		[BsonElement("Name")]
		public string Name { get; set; }
		[BsonElement("Color")]
		public string Color { get; set; }
		[BsonElement("Path")]
		public string Path { get; set; }
		[BsonElement("Language")]
		public Language Language { get; set; }
		[BsonElement("Company")]
        public Company Company { get; set; }
		[BsonElement("SearchEngines")]
		public Searchengines SearchEngines { get; set; }
		[BsonElement("Socialmedia")]
		public Socialmedia SocialMedia { get; set; }
		[BsonElement("Controllers")]
		public Controllers Controllers { get; set; }
	}
	
	public class Language
	{
		public string[] Locale { get; set; }
	}

    public class Company
    {
        public string Name { get; set; }
        public Address Address { get; set; }
        public Contact Contact { get; set; }
        public Contact Service { get; set; }
        public Geolocation Geolocation { get; set; }
    }

    public class Address
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public string Extra { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }

    public class Contact
    {
        public string Name { get; set; }
        public string Function { get; set; }
        public string Phonenumber { get; set; }
        public string Mobilenumber { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
    }

    public class Geolocation
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }    
    }

	public class Searchengines
	{
		public string Googleverification { get; set; }
		public bool GoogleTranslation { get; set; }
		public string WebsiteType { get; set; }
		public string Author { get; set; }
		public string Generator { get; set; }
		public Visibility Visibility { get; set; }
	}

	public class Visibility
	{
		public int PageVisibilityPublic { get; set; }
		public int PageVisibilityLimited { get; set; }
		public int PageVisibilityBlocked { get; set; }
	}

	public class Socialmedia
	{
		public bool SocialMediaTags { get; set; }
		public Media Media { get; set; }
	}

	public class Media
	{
		public string Facebook { get; set; }
		public string Twitter { get; set; }
		public string Googleplus { get; set; }
		public string Linkedin { get; set; }
		public string Youtube { get; set; }
		public string Pinterest { get; set; }
	}

	public class Controllers
	{
		public Standard Standard { get; set; }
		public Article Article { get; set; }
		public Navigation Navigation { get; set; }
	}

	public class Standard
	{
		public bool Active { get; set; }
		public bool Admin { get; set; }
		public bool Maintenance { get; set; }
	}

	public class Article
	{
		public bool Active { get; set; }
		public bool Admin { get; set; }
		public string[] Tools { get; set; }
		public string[] Actions { get; set; }
		public string[] Templates { get; set; }
	}

	public class Navigation
	{
		public bool Active { get; set; }
		public bool Admin { get; set; }
		public int MaxLength { get; set; }
		public Placement Placement { get; set; }
	}

	public class Placement
	{
		public int Disabled { get; set; }
		public int AllNavigations { get; set; }
		public int TopNavigationOnly { get; set; }
		public int LeftNavigationOnly { get; set; }
		public int BottomNavigationOnly { get; set; }
		public int AllExceptTopNavigation { get; set; }
		public int _301Redirect { get; set; }
	}
}
