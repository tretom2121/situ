namespace Situ.Models
{
    public class User
    {
        public User()
        {
            Id = int.MinValue;
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{LastName}, {FirstName}";
        public string Email { get; set; } = string.Empty;
        public bool Admin { get; set; }
    }
}
