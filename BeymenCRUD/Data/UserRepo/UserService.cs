namespace BeymenCRUD.Data.UserRepo
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _appDbContext;

        // Constructor
        // Inject AppDbContext inside the constructor to make access to the AppDbContext 
        public UserService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IQueryable<User> GetUsers()
        {
            IQueryable<User> allUsers = _appDbContext.User;
            return allUsers;
        }

        public User DetailsUser(string id)
        {

            var user = _appDbContext.User.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                throw new Exception("User not found");
            }
            return user;
        }

        public async Task<User> AddUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user", "User cannot be null");
            }
            await _appDbContext.User.AddAsync(user);
            await _appDbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUser(User user, string id)
        {
            var userDetails = DetailsUser(id);

            if (user != null)
            {
                userDetails.FirstName = user.FirstName;
                userDetails.LastName = user.LastName;
                userDetails.Email = user.Email;
                userDetails.Password = user.Password;

                userDetails.UpdatedTime = DateTime.UtcNow.AddHours(3);

                await _appDbContext.SaveChangesAsync();
            }
            return userDetails;
        }

        public bool DeleteUser(string id)
        {
            var user = DetailsUser(id);

            if (user != null)
            {
                _appDbContext.User.Remove(user);
                _appDbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public IQueryable<User> TakeUsers(IQueryable<User> query, int? count)
        {

            if (count.HasValue && count >= 0)
            {
                int countValue = count.Value; // Convert nullable int to non-nullable int
                query = query.Take(countValue);
            }

            return query;
        }

        public IQueryable<User> FilterUsers(IQueryable<User> query, string? firstName, string? lastName)
        {
            if (!string.IsNullOrEmpty(firstName))
            {
                query = query.Where(u => u.FirstName == firstName);
            }
            if (!string.IsNullOrEmpty(lastName))
            {
                query = query.Where(u => u.LastName == lastName);
            }

            return query;
        }

        public IQueryable<User> UserStatus(IQueryable<User> query, string? userStatus)
        {

            if (userStatus != null)
            {
                if (userStatus == "all")
                {
                    query = GetUsers();
                }
                else if (userStatus == "active" || userStatus == "deactive")
                {
                    query = query.Where(u => u.UserStatus == userStatus);
                }
                else
                {
                    throw new Exception("Invalid input");
                }
            }
            return query;
        }

    }
}
