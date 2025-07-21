using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Finder { get; set; }

        [Required]
        [StringLength(100)]
        public string Heading { get; set; }

        [Required]
        [StringLength(50)]
        public string By { get; set; }

        [Required]
        [StringLength(255)]
        public string Thumbnail { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "In Progress";

        [Required]
        [StringLength(500)]
        public string ShortDescription { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; } = DateTime.UtcNow;

        [Required]
        [StringLength(100)]
        public string Link { get; set; }

        [Required]
        [StringLength(20)]
        public string Type { get; set; } = "project";

        public bool isCarousel { get; set; } = false;
        
        public virtual TranslationProgress? TranslationProgress { get; set; }
        public virtual ProjectDetail? Detail { get; set; }
    }
}