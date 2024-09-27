using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using login.Models.Entities;
using login.Models.Enums;
using login.Models.DTO.User;

namespace login.Services.UserService
{
    public class UserService : IUserService
    {

        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<Users> _userCollection;
        public UserService(IConfiguration configuration)
        {
            _configuration = configuration;
            var mongoDBSettings = _configuration.GetSection("MongoDBSettings").Get<MongoDBSettings>();
            MongoClientSettings settings = MongoClientSettings.FromConnectionString(mongoDBSettings.ConnectionString);
            settings.RetryWrites = false;
            MongoClient client = new MongoClient(settings);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.DatabaseName);
            _userCollection = database.GetCollection<Users>("User");
        }

        public async Task<IEnumerable<Users>> GetUsersAsync()
        {
            var filter = Builders<Users>.Filter.Empty;

            var usersCursor = await _userCollection.FindAsync(filter);
            return await usersCursor.ToListAsync();
        }

        public async Task<Users> GetUserByIdAsync(string id)
        {
            var filter = Builders<Users>.Filter.Eq("_id", ObjectId.Parse(id));
            var result = await _userCollection.FindAsync(filter);
            return await result.FirstOrDefaultAsync();
        }
        public async Task<(Users user, string message)> GetAuthenticatedUserAsync(Login login)
        {
            var filter = Builders<Users>.Filter.Eq("userID", login.Username);
            var user = await _userCollection.Find(filter).FirstOrDefaultAsync();
            if (user != null && user.Password == login.Password)
            {
                return (user, "Authentication successful");
            }
            if (user == null)
            {
                return (null, "User not found");
            }
            else
            {
                return (null, "Incorrect password");
            }
        }

        public async Task DeleteUserAsync(string id)
        {
            var filter = Builders<Users>.Filter.Eq("_id", ObjectId.Parse(id));
            await _userCollection.DeleteOneAsync(filter);
        }

        public async Task CreateUserAsync(Users user)
        {
            await _userCollection.InsertOneAsync(user);

        }

        public async Task CreateBulkUserAsync(bool deletePreviousRecord, int totalRecord)
        {
            var filter = Builders<Users>.Filter.Empty;
            if (deletePreviousRecord)
                _userCollection.DeleteMany(filter);

            List<Users> users = GenerateRandomUsers(totalRecord);
            await _userCollection.InsertManyAsync(users);
        }
        public List<Users> GenerateRandomUsers(int count)
        {
            var users = new List<Users>();
            var random = new Random();
            for (int i = 0; i < count; i++)
            {
                Users user = new Users();

                user.Id = ObjectId.GenerateNewId().ToString();

                user.FirstName = GetRandomName();
                user.LastName = GetRandomName();
                user.Roles = GetRandomRoles(i);
                if (i < 250)
                {
                    user.Email = $"user{i + 1}@example.com";
                    user.UserID = $"user{i + 1}";
                    user.Password = $"Password@{i + 1}";
                }
                else
                {
                    user.Email = user.FirstName + "." + user.LastName + "@company.com";
                    user.UserID = user.FirstName + "." + user.LastName;
                    user.Password = user.FirstName + "@" + user.LastName + $"{i + 1}";
                }

                user.ProfilePic = $"profile_pic_{i + 1}.jpg";
                user.Company = $"Company{i + 1}";
                user.JobTitle = $"JobTitle{i + 1}";
                user.OfficeNumber = $"Office{i + 1}";
                user.PhoneNumber = $"123-456-{i.ToString("D3")}";
                user.AlternateName = $"AltName{i + 1}";
                user.AlternateNumber = $"789-012-{i.ToString("D3")}";
                user.IsLocked = random.Next(2) == 0;
                user.IsPasswordReset = random.Next(2) == 0;
                user.InvalidPasswordAttempts = random.Next(10);
                user.LastLoginDate = DateTime.Now.AddMinutes(-random.Next(1, 10080));
                user.PasswordHistory = GenerateRandomPasswordHistory();

                users.Add(user);
            }

            return users;
        }

        public string GetRandomName()
        {
            var names = new[] { "George", "Nancy", "John", "Jane", "Alice", "Bob", "Charlie", "David", "Emma", "Frank", "Grace", "Henry" };
            var random = new Random();
            return names[random.Next(names.Length)];
        }

        private static IEnumerable<Role> GetRandomRoles(int i)
        {
            List<Role> randomRoles = new List<Role>();
            if (i == 0)
            {
                var allRoles = Enum.GetValues(typeof(Role)).Cast<Role>().ToList();
                var adminRole = allRoles.FirstOrDefault(x => x == Role.Admin);
                return adminRole != Role.WellControl ? new List<Role> { adminRole } : new List<Role>();
            }
            else
            {
                var allRoles = Enum.GetValues(typeof(Role)).Cast<Role>().ToList();
                var random = new Random();
                int numberOfRoles = random.Next(1, 5);
                return allRoles.OrderBy(_ => random.Next()).Take(numberOfRoles);
            }
        }
        public string[] GenerateRandomPasswordHistory()
        {
            var passwordHistory = new List<string>();
            var random = new Random();
            for (int i = 0; i < random.Next(1, 5); i++)
            {
                passwordHistory.Add($"Password@{i + 1}");
            }
            return passwordHistory.ToArray();
        }


        public async Task UpdateUserAsync(string id, Users user)
        {
            var filter = Builders<Users>.Filter.Eq("_id", ObjectId.Parse(id));

            var update = Builders<Users>.Update
                .Set(u => u.FirstName, user.FirstName)
                .Set(u => u.LastName, user.LastName)
                .Set(u => u.Email, user.Email)
                .Set(u => u.ProfilePic, user.ProfilePic)
                .Set(u => u.Roles, user.Roles)
                .Set(u => u.Company, user.Company)
                .Set(u => u.JobTitle, user.JobTitle)
                .Set(u => u.OfficeNumber, user.OfficeNumber)
                .Set(u => u.PhoneNumber, user.PhoneNumber)
                .Set(u => u.AlternateName, user.AlternateName)
                .Set(u => u.AlternateNumber, user.AlternateNumber)
                .Set(u => u.IsLocked, user.IsLocked)
                .Set(u => u.Password, user.Password)
                .Set(u => u.IsPasswordReset, user.IsPasswordReset)
                .Set(u => u.InvalidPasswordAttempts, user.InvalidPasswordAttempts)
                .Set(u => u.LastLoginDate, user.LastLoginDate)
                .Set(u => u.PasswordHistory, user.PasswordHistory);

            await _userCollection.UpdateOneAsync(filter, update);
        }

        public async Task<bool> UpdateUserPassword(UpdatePassword userPassword)
        {
            var filter = Builders<Users>.Filter.Eq("_id", ObjectId.Parse(userPassword.Id));

            var update = Builders<Users>.Update.Set(u => u.Password, userPassword.Password);
            var result = await _userCollection.UpdateOneAsync(filter, update);
            return result.IsModifiedCountAvailable;
        }
        public async Task<bool> UpdateUserPermission(string id, IEnumerable<Role> roles)
        {
            var filter = Builders<Users>.Filter.Eq("_id", ObjectId.Parse(id));

            var update = Builders<Users>.Update
                .Set(u => u.Roles, roles);

            UpdateResult result = await _userCollection.UpdateOneAsync(filter, update);
            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
