namespace Web.ViewComponents
{
	using Library.Config;
	using Library.Models;
	using Library.Repositories;

	using Microsoft.AspNetCore.Mvc;

	using System;
	using System.Collections.Generic;

	public class SocialViewComponent : ViewComponent
	{
		private readonly SiteConfig _config;

		public SocialViewComponent(ConfigRepository config)
		{
			_config = config.GetConfig();
		}

		public IViewComponentResult Invoke()
		{
			var social = _config.SocialMedia.Media;
			//TODO: Needs to be done in an other way
			var socialList = new List<string>();
				socialList.Add(social.Linkedin);
				socialList.Add(social.Dribbble);
				socialList.Add(social.Facebook);

			var items = new List<TextLink>();

			foreach(var item in socialList){
				if(item != ""){
					var title = item.Split('.')[1];

					items.Add(new TextLink(){
						Title = title,
						Url = item,
						Target = "_blank"
					});
				}
			}

			return View(items);
		}
	}
}