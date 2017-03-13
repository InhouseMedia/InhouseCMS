namespace Library.Repositories
{
	using Library.Models;
	using Library.Connections;

	using System.Collections.Generic;
	using System.Net.Http;
	using System.Threading.Tasks;

	public interface IProductRepository
	{
		Task<IEnumerable<Product>> List();
		Task<Product> GetById(string productId);
		//Task<ArticlePage> GetPage(string productId);
		//Task<ArticleContent> GetPageContent(string articleId, string contentId);
	}

	public class ProductRepository : ConnectionRepository, IProductRepository
	{
		public ProductRepository(ApiConnection api) : base(api)
		{
		}

		public async Task<IEnumerable<Product>> List()
		{
			var conn = await _api.ConnectAsync("/product");
			var result = conn.Content.ReadAsAsync<IEnumerable<Product>>();
			return await result;
		}

		public async Task<Product> GetById(string productId)
		{
			var conn = await _api.ConnectAsync("/product/" + productId);
			var result = conn.Content.ReadAsAsync<Product>();
			return await result;
		}
	}
}