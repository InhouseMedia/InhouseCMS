namespace Web.Repositories
{
	using System.Net.Http;
	using System.Threading.Tasks;

	using Library.Models;
    using Web.Connections;

    public interface IArticleRepository
	{
		Task<Article> GetById(string articleId);
		Task<ArticlePage> GetPage(string articleId);
	}

	public class ArticleRepository : ConnectionRepository, IArticleRepository
	{
	    public ArticleRepository(ApiConnection api) : base(api)
	    {
		}

		public async Task<Article> GetById(string articleId)
		{
			var conn = await _api.ConnectAsync("/article/" + articleId);
			var test = conn.Content.ReadAsAsync<Article>();
			return await test;
		}

		public async Task<ArticlePage> GetPage(string articleId)
		{
			var conn = await _api.ConnectAsync("/article/" + articleId + "/content");
			var test = conn.Content.ReadAsAsync<ArticlePage>();
			return await test;
		}
	}
}