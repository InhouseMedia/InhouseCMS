namespace Web.Helpers
{
	using Library.Models;

	using Microsoft.AspNetCore.Html;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc.Rendering;
	using Microsoft.AspNetCore.Razor.TagHelpers;

	using System.Collections.Generic;
	using System.Linq;
	using System.Text.Encodings.Web;

	[HtmlTargetElement("treeview", Attributes = "tree-list")]
	public class TreeViewTagHelper : TagHelper
	{
		[HtmlAttributeName("tree-list")]
		public IEnumerable<NavigationSitemap> Sitemap { get; set; }
		private static string _url { get; set; }
		public TreeViewTagHelper(IHttpContextAccessor httpContextAccessor){
			_url = httpContextAccessor.HttpContext.Request.Path.Value;
		}
		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			output.TagName = "nav";
			output.Content.SetHtmlContent(CreateSitemap(Sitemap, true));
		}

		private static TagBuilder CreateSitemap(IEnumerable<NavigationSitemap> sitemap , bool isRoot = false)
		{
			var ul = new TagBuilder("ul");
				ul.AddCssClass(isRoot ? "menu":"subItem");

			foreach (var item in sitemap)
			{
				var li = new TagBuilder("li");
				var a = new TagBuilder("a");

				a.AddCssClass(_url == item.Url ? "active": "");
				a.Attributes["href"] = item.Url ?? "#";
				a.InnerHtml.SetHtmlContent(item.Title);

				li.InnerHtml.AppendHtml(a);

				if (item.ChildLocations.Any())
				{
					var map = CreateSitemap(item.ChildLocations);
					var content = GetString(map);
					li.InnerHtml.AppendHtml(content);
				}

				ul.InnerHtml.AppendHtml(li);
			}

			return ul;
		}

		public static string GetString(TagBuilder content)
		{
			var writer = new System.IO.StringWriter();
			content.WriteTo(writer, HtmlEncoder.Default);
			return writer.ToString();
		}
	}
}