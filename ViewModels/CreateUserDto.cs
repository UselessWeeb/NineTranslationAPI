namespace ViewModels
{
    public class CreateUserDto
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? DisplayName { get; set; }
        public DateTime JoinDate { get; set; } = DateTime.UtcNow;
        public string? ProfilePictureUrl { get; set; }
        public string Role { get; set; } = "User";
    }
}
