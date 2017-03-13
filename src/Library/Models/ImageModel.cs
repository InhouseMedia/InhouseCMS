namespace Library.Models
{
	public class ImageSettings
	{
		public string size { get; set; }
		public int width { get; set; }
		public float s_brightness { get; set; }
		public float s_contrast { get; set; }
		public float s_saturation { get; set; }
		public string s_grayscale { get; set; }
		public bool s_sepia { get; set; }
		public string sflip { get; set; }
		public string watermark { get; set; }
		public string title { get; set; }
	}

	public class ImageModel
	{
		public Image Image { get; set; }
		public Color[] Color { get; set; }
		public Filters Filters { get; set; }
		public Transform Transform { get; set; }
		public Watermark Watermark { get; set; }
	}

	public class Image
	{
		public string ImageId { get; set; }
		public string Url { get; set; }
		public string Title { get; set; }
		public string Alt { get; set; }
		public bool Enlarge { get; set; }
		public string LinkTo { get; set; }
		public string Size { get; set; }
		public Placement[] Placement { get; set; }
	}

	public class Placement
	{
		public string Name { get; set; }
		public bool Value { get; set; }
	}

	public class Color
	{
		public string Name { get; set; }
		public float Value { get; set; }
	}

	public class Filters
	{
		public sGrayscale[] S_Grayscale { get; set; }
	}

	public class sGrayscale {
		public string Name { get; set; }
		public bool Value { get; set; }
	}

	public class Transform
	{
		public Sflip[] Sflip { get; set; }
	}

	public class Sflip
	{
		public string Name { get; set; }
		public bool Value { get; set; }
	}

	public class Watermark
	{
		public Overlay[] Overlay { get; set; }
		public Title Title { get; set; }
	}

	public class Overlay {
		public string Name { get; set; }
		public bool Value { get; set; }
	}

	public class Title
	{
		public bool Active { get; set; }
		public string Value { get; set; }
	}
}