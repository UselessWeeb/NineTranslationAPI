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

        public ICollection<ProjectStaff> StaffRoles { get; set; }
    }
}