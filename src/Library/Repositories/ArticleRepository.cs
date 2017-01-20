namespace Library.Repositories
{
	using Library.Models;
    using Library.Connections;

	using System.Net.Http;
	using System.Threading.Tasks;

    public interface IArticleRepository
	{
		Task<Article> GetById(string articleId);
		Task<ArticlePage> GetPage(string articleId);
		Task<ArticleContent> GetPageContent(string articleId, string contentId);
	}

	public class ArticleRepository : ConnectionRepository, IArticleRepository
	{
	    public ArticleRepository(ApiConnection api) : base(api)
	    {
		}

		public async Task<Article> GetById(string articleId)
		{
			var conn = await _api.ConnectAsync("/article/" + articleId);
			var result = conn.Content.ReadAsAsync<Article>();
			return await result;
		}

		public async Task<ArticlePage> GetPage(string articleId)
		{
			var conn = await _api.ConnectAsync("/article/" + articleId + "/content");
			var result = conn.Content.ReadAsAsync<ArticlePage>();
			return await result;
		}

		public async Task<ArticleContent> GetPageContent(string articleId, string contentId)
		{
			var conn = await _api.ConnectAsync("/article/" + articleId + "/content/" + contentId);
			var result = conn.Content.ReadAsAsync<ArticleContent>();
			return await result;
		}
	}
}