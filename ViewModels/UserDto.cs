namespace ViewModels
{
    public class UserDto
    {
        public string? Id { get; set; }
        public string? DisplayName { get; set; }
        public string? Email { get; set; }
        public IList<string>? Roles { get; set; }
        public bool isActive { get; set; }
    }
}