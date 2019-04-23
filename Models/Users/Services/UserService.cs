using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using API;
using MongoDB.Driver;

namespace Models.Users.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> users;

        public UserService(Configuration config)
        {
            var client = new MongoClient(config.GetConnectionString("TodoStoreDb"));
            var database = client.GetDatabase("TodoStoreDb");
            users = database.GetCollection<User>("Users");
        }

        public Task<List<User>> GetAsync()
        {
            var search = users.Find(user => true);
            var result = search.ToList();

            return Task.FromResult(result);
        }

        public Task<User> GetAsync(string login)
        {
            var search = users.Find(user => user.Login == login);
            var result = search.FirstOrDefault();

            if (result is null)
            {
                throw new UserNotFoundException(login);
            }

            return Task.FromResult(result);
        }

        public Task<User> CreateAsync(UserCreationInfo creationInfo, CancellationToken cancellationToken)
        {
            if (creationInfo == null)
            {
                throw new ArgumentNullException(nameof(creationInfo));
            }

            cancellationToken.ThrowIfCancellationRequested();

            var countUsersWithSameLogin = users.Find(usr => usr.Login == creationInfo.Login).CountDocuments();

            if (countUsersWithSameLogin > 0)
            {
                throw new UserDuplicationException(creationInfo.Login);
            }

            var user = new User
            {
                Login = creationInfo.Login,
                PasswordHash = creationInfo.PasswodHash,
                RegisteredAt = DateTime.Now
            };
            
            users.InsertOne(user);

            return Task.FromResult(user);
        }

        public Task UpdateAsync(string id, User userIn)
        {
            users.ReplaceOne(user => user.Id == id, userIn);

            return Task.CompletedTask;
        }

        public Task RemoveAsync(User userIn)
        {
            users.DeleteOne(user => user.Id == userIn.Id);

            return Task.CompletedTask;
        }

        public Task RemoveAsync(string id)
        {
            users.DeleteOne(user => user.Id == id);

            return Task.CompletedTask;
        }
    }
}