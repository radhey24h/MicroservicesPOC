using Users.Models.DTO.User;
using Users.Models.Entities;
using Users.Models.Enums;
using MongoDB.Bson;
using MongoDB.Driver;
using Amazon.Runtime;

namespace Users.Services.UserService
{
    public class UserService : IUserService
    {

        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<User> _userCollection;
        private readonly IHttpClientFactory _httpClientFactory;

        public UserService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            var mongoDBSettings = _configuration.GetSection("MongoDBSettings").Get<MongoDBSettings>();
            MongoClientSettings settings = MongoClientSettings.FromConnectionString(mongoDBSettings.ConnectionString);
            settings.RetryWrites = false;
            MongoClient client = new MongoClient(settings);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.DatabaseName);
            _userCollection = database.GetCollection<User>("User");
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var filter = Builders<User>.Filter.Empty;

            var usersCursor = await _userCollection.FindAsync(filter);
            return await usersCursor.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            var filter = Builders<User>.Filter.Eq("_id", ObjectId.Parse(id));
            var result = await _userCollection.FindAsync(filter);
            return await result.FirstOrDefaultAsync();
        }
        public async Task<(User user, string message)> GetAuthenticatedUserAsync(Login login)
        {
            var filter = Builders<User>.Filter.Eq("emailId", login.EmailId);
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
            var filter = Builders<User>.Filter.Eq("_id", ObjectId.Parse(id));
            await _userCollection.DeleteOneAsync(filter);
        }

        public async Task CreateUserAsync(User user)
        {
            await _userCollection.InsertOneAsync(user);
            //
            // use http client to send the message either using broker or httpclient 
            //
            
            SendEmailToUser(user);
        }

        private async void SendEmailToUser(User user)
        {
            var mailRequest = new
            {
                Name = user.FirstName + " "+ user.LastName,
                Email = user.Email,
                Subject = user.Subject,
                PhoneNumber = user.PhoneNumber,
                UserType = user.UserType
            };

            // Use HttpClient to send the message to the mailer API
            var client = _httpClientFactory.CreateClient("MailerApi");

            try
            {
                // Send a POST request to the mailer API
                var response = await client.PostAsJsonAsync("mailerapi/sendEmail", mailRequest);

                if (!response.IsSuccessStatusCode)
                {
                    // Handle the error (e.g., log it, throw an exception, etc.)
                    throw new Exception("Failed to send email.");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions that might occur during the HTTP request
                // Log the error or take appropriate action
                throw new Exception("Error occurred while sending email: " + ex.Message);
            }
        }

        public async Task CreateBulkUserAsync(bool deletePreviousRecord, int totalRecord)
        {
            var filter = Builders<User>.Filter.Empty;
            if (deletePreviousRecord)
                _userCollection.DeleteMany(filter);

            List<User> users = GenerateRandomUsers(totalRecord);
            await _userCollection.InsertManyAsync(users);
        }
        public List<User> GenerateRandomUsers(int count)
        {
            var users = new List<User>();
            var random = new Random();
            for (int i = 0; i < count; i++)
            {
                User user = new User();

                user.Id = ObjectId.GenerateNewId().ToString();

                user.FirstName = GetRandomName();
                user.LastName = GetRandomName();
                user.Roles = GetRandomRoles(i);
                if (i < 250)
                {
                    user.Email = $"user{i + 1}@example.com";
                    user.Password = $"Password@{i + 1}";
                }
                else
                {
                    user.Email = user.FirstName + "." + user.LastName + "@company.com";
                    user.Password = user.FirstName + "@" + user.LastName + $"{i + 1}";
                }

                user.PhoneNumber = $"123-456-{i.ToString("D3")}";
                user.IsLocked = random.Next(2) == 0;

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
                return adminRole != Role.Admin ? new List<Role> { adminRole } : new List<Role>();
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


        public async Task UpdateUserAsync(string id, User user)
        {
            var filter = Builders<User>.Filter.Eq("_id", ObjectId.Parse(id));

            var update = Builders<User>.Update
                .Set(u => u.FirstName, user.FirstName)
                .Set(u => u.LastName, user.LastName)
                .Set(u => u.Email, user.Email)
                .Set(u => u.Roles, user.Roles)
                .Set(u => u.PhoneNumber, user.PhoneNumber)
                .Set(u => u.IsLocked, user.IsLocked)
                .Set(u => u.Password, user.Password);

            await _userCollection.UpdateOneAsync(filter, update);
        }

        public async Task<bool> UpdateUserPassword(UpdatePassword userPassword)
        {
            var filter = Builders<User>.Filter.Eq("emailId", ObjectId.Parse(userPassword.EmailId));

            var update = Builders<User>.Update.Set(u => u.Password, userPassword.Password);
            var result = await _userCollection.UpdateOneAsync(filter, update);
            return result.IsModifiedCountAvailable;
        }

    }
}
