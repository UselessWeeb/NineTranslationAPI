using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class TranslationProgress
    {
        [Key]
        public int Id { get; set; }

        [Range(0, 100)]
        public decimal Translate { get; set; }

        [Range(0, 100)]
        public decimal Edit { get; set; }

        [Range(0, 100)]
        public decimal QA { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}