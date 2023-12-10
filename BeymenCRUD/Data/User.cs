namespace BeymenCRUD.Data
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreationTime { get; set; } = DateTime.UtcNow.AddHours(3);
        public DateTime UpdatedTime { get; set; } = DateTime.UtcNow.AddHours(3);
        public string UserStatus { get; set; } = "active";

        // #######################################################################

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;


    }
}
