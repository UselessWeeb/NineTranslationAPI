using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class ProjectStaff
    {
        public int Id { get; set; }
        public int ProjectDetailId { get; set; }
        public string UserId { get; set; }
        public StaffRoleType Role { get; set; }

        public ProjectDetail ProjectDetail { get; set; }
        public User User { get; set; }
    }

    public enum StaffRoleType
    {
        ProjectManager,
        Translator,
        Editor,
        UI,
        Tech,
        QA
    }
}