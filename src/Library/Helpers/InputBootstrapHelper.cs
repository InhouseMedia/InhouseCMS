namespace Library.Helpers
{
	using Library.Models;

	using Microsoft.AspNetCore.Html;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc.Rendering;
	using Microsoft.AspNetCore.Razor.TagHelpers;

	using System.Collections.Generic;
	using System.Linq;
	using System.Text.Encodings.Web;

	[HtmlTargetElement("input", Attributes = "tree-list")]
	public class InputBootstrapTagHelper : TagHelper
	{

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{


		}
	}
}