namespace Api.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MongoDB.Driver;
    using MongoDB.Bson;

    using Api.Models;

    public interface IUserRepository
    {
        Task<IEnumerable<User>> Users();
        Task<User> GetById(ObjectId id);
        Task<User> Login(string username, string password);
    }

    public class UserRepository : IUserRepository
    {
        private readonly IMongoDatabase _database;
		
        public UserRepository(DataAccess access)
        {
            _database = access.Connect();
        }
		
        public async Task<IEnumerable<User>> Users()
        {
            var test = _database.GetCollection<User>("User");
            var temp = await test.Find(_=>true).ToListAsync();
            return temp.ToArray();
        }
		
		public async Task<User> GetById(ObjectId id)
        {
			var builder = Builders<User>.Filter; 
            var filter = builder.Eq("Id", id);
			var conn = _database.GetCollection<User>("User");
			var temp = await conn.Find(filter).FirstOrDefaultAsync();
			return temp;	
        }
		
        public async Task<User> Login(string username, string password) {
			//string hashedPassword = Crypto.HashPassword(password);
			//	bool verify = Crypto.VerifyHashedPassword(hashedPassword, password);
				
			//Console.Write(" PasswordHash " + hashedPassword + " Verify " + verify.ToString());
            //var filter = Builders<User>.Filter.Eq("UserName", username);
            //var filter = Builders<User>.Filter.Where(x => x.UserName.Equals(username) && x.Password.Equals(password));
            var filter = Builders<User>.Filter.Where(x => x.UserName.Equals(username));
			var test = _database.GetCollection<User>("User");
            var temp = await test.Find(filter).SingleOrDefaultAsync();
            return temp;
        }
        
        /*
        public bool Remove(ObjectId id)
        {
            var query = Query<User>.EQ(e => e.Id, id);
            var result = _database.GetCollection<User>("user").Remove(query);

            return GetById(id) == null;
        }

        public void Update(User user)
        {
            var query = Query<User>.EQ(e => e.Id, user.Id);
            var update = Update<User>.Replace(user); // update modifiers
            _database.GetCollection<User>("user").Update(query, update);
        }
        */
    }
}