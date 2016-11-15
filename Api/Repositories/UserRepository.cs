namespace Api.Repositories
{
	using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    
    using Microsoft.Extensions.Options;

    using MongoDB.Driver;

    using Api.Models;

    public interface IUserRepository
    {
        Task<IEnumerable<User>> AllUsers();
        
        Task<User> Login(string username, string password);
    }

    public class UserRepository : IUserRepository
    {
        private readonly Settings _settings;
        private readonly IMongoDatabase _database;
		readonly DateTime? _today;
		
        public UserRepository(IOptions<Settings> settings)
        {
            _settings = settings.Value;
            _database = Connect();
			_today = DateTime.UtcNow;
        }

        /*
        public void Add(User user)
        {
            _database.GetCollection<User>("user").Save(user);
        }
		*/
		
        public async Task<IEnumerable<User>> AllUsers()
        {
            var test = _database.GetCollection<User>("User");
            var temp = await test.Find(_=>true).ToListAsync();
            return temp.ToArray();
        }
		
		/*
        public User GetById(ObjectId id)
        {
            var query = Query<User>.EQ(e => e.Id, id);
            return _database.GetCollection<User>("user").FindOne(query);
        }
        */
		
        public async Task<User> Login(string username, string password) {
			//string hashedPassword = Crypto.HashPassword(password);
			//	bool verify = Crypto.VerifyHashedPassword(hashedPassword, password);
				
			//Console.Write(" PasswordHash " + hashedPassword + " Verify " + verify.ToString());
            //var filter = Builders<User>.Filter.Eq("UserName", username);
            //var filter = Builders<User>.Filter.Where(x => x.UserName.Equals(username) && x.Password.Equals(password));
            var filter = Builders<User>.Filter.Where(x => x.UserName.Equals(username));
			var test = _database.GetCollection<User>("User");
            var temp = await test.Find(filter).SingleOrDefaultAsync();
            return temp as User;
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

        private IMongoDatabase Connect()
        {
            var client = new MongoClient(_settings.MongoConnection);
            var database = client.GetDatabase(_settings.Database);
            return database;
        }
    }
}