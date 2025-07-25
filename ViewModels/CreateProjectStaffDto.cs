using Models.Enums;

namespace ViewModels
{
    public class CreateProjectStaffDto
    {
        public int ProjectDetailId { get; set; }
        public string? UserId { get; set; }
        public StaffRoleType Role { get; set; }
    }
}