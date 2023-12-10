namespace BeymenCRUD.Data.UserRepo
{
    public interface IUserService
    {
        IQueryable<User> GetUsers();
        User DetailsUser(string id);
        Task<User> AddUser(User user);
        Task<User> UpdateUser(User user, string id);
        bool DeleteUser(string id);
        public IQueryable<User> TakeUsers(IQueryable<User> query, int? count);
        public IQueryable<User> FilterUsers(IQueryable<User> query, string? firstName, string? lastName);
        public IQueryable<User> UserStatus(IQueryable<User> query, string? userStatus);

    }
}
