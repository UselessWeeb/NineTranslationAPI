namespace APINineTranslation.Controllers
{
    public class UpdateUserDto
    {
        public required string Id { get; set; }
        public required string DisplayName { get; set; }
        public required string Email { get; set; }
        public required string NewPassword { get; set; }
    }
}