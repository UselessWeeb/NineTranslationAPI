using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string DisplayName { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime JoinDate { get; set; } = DateTime.UtcNow;

        public string ProfilePicture { get; set; } = "default-profile.png";

        public virtual ICollection<ProjectStaff>? StaffRoles { get; set; }
    }
}