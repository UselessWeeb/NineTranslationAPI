using Models.Enums;

namespace ViewModels
{
    public class CreateProjectStaffDto
    {
        public string? UserId { get; set; }
        public StaffRoleType Role { get; set; }
    }
}