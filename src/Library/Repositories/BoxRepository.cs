﻿namespace Library.Repositories
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Net.Http;

	using Library.Models;
	using Library.Connections;

	public interface IBoxRepository
	{
		IEnumerable<Box> BoxList(int placement, string articleId, string template);
	}

	public class BoxRepository : ConnectionRepository, IBoxRepository
	{
		private readonly IEnumerable<Box> _boxes;

		public BoxRepository(ApiConnection api) : base(api)
		{
			var conn = _api.Connect("box/list");
			var result = conn.Content.ReadAsAsync<IEnumerable<Box>>();
			_boxes = result.Result;
		}

		public IEnumerable<Box> BoxList(int placement, string articleId, string template)
		{
			return _boxes?.Where(b => b.Placement.Equals(placement) && (b.ArticleId == articleId || b.ArticleId == null) && (b.Template == template.ToLower() || b.Template == null)).ToList();
		}
	}
}