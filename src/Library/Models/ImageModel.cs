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
		//[Display(Name = "ImageSettingsImage", ResourceType = typeof(Translate))]
		public Image Image { get; set; }
		//[Display(Name = "ImageSettingsColor", ResourceType = typeof(Translate))]
		public Color[] Color { get; set; }
		//[Display(Name = "ImageSettingsFilters", ResourceType = typeof(Translate))]
		public Filters Filters { get; set; }
		//[Display(Name = "ImageSettingsTransform", ResourceType = typeof(Translate))]
		public Transform Transform { get; set; }
		//[Display(Name = "ImageSettingsWatermark", ResourceType = typeof(Translate))]
		public Watermark Watermark { get; set; }
	}

	public class Image
		{
			public string ImageId { get; set; }
			public string Url { get; set; }
			//[Display(Name = "ImageSettingsTitle", ResourceType = typeof(Translate))]
			public string Title { get; set; }
			public string Alt { get; set; }
			//[Display(Name = "ImageSettingsEnlarge", ResourceType = typeof(Translate))]
			public bool Enlarge { get; set; }
			//[Display(Name = "ImageSettingsLinkTo", ResourceType = typeof(Translate))]
			public string LinkTo { get; set; }
			//[Display(Name = "ImageSettingsSize", ResourceType = typeof(Translate))]
			public string Size { get; set; }
			//[Display(Name = "ImageSettingsPlacement", ResourceType = typeof(Translate))]
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
			//[Display(Name = "ImageSettingsFlip", ResourceType = typeof(Translate))]
			public Sflip[] Sflip { get; set; }
		}

		public class Sflip
		{
			public string Name { get; set; }
			public bool Value { get; set; }
		}

		public class Watermark
		{
			//[Display(Name = "ImageSettingsOverlay", ResourceType = typeof(Translate))]
			public Overlay[] Overlay { get; set; }
			//[Display(Name = "ImageSettingsTitle", ResourceType = typeof(Translate))]
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



	/*
	public class ImageModel
	{
		public string Src { get; set; }
		public string Alt { get; set; }
		public string Caption { get; set; }
		public string Align { get; set; }
		public bool Share { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
	}*/
}