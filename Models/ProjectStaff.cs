using Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class ProjectStaff
    {
        public int Id { get; set; }
        public int ProjectDetailId { get; set; }
        public string UserId { get; set; }
        public StaffRoleType Role { get; set; }

        public virtual ProjectDetail ProjectDetail { get; set; }
        public virtual User User { get; set; }
    }
}