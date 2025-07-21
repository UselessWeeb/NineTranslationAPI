using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class PatchUpdate
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Version { get; set; }

        [Required]
        public string Detail { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ReleaseDate { get; set; } = DateTime.UtcNow;

        // Foreign key
        public int ProjectDetailId { get; set; }
        public virtual ProjectDetail ProjectDetail { get; set; }
    }
}