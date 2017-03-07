namespace Library.Models
{
	public class SectionModel
	{
		public string Image { get; set; }
		public string Title { get; set; }
		public string Text { get; set; }
		public TextLink Link{ get; set; }

		public class TextLink
		{
			public string Url{ get; set; }
			public string Target { get; set; }
			public string Title { get; set; }
		}
	}
}