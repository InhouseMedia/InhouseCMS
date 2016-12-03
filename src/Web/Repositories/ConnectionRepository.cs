namespace Web.Repositories
{
    using Web.Connections;

    public class ConnectionRepository
    {
        protected readonly ApiConnection _api;

        public ConnectionRepository(ApiConnection api)
        {
            _api = api;
        }
    }
}