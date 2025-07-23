using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class CreateTranslationProgressDto
    {
        [Range(0, 100)]
        public decimal Translate { get; set; }
        [Range(0, 100)]
        public decimal Edit { get; set; }
        [Range(0, 100)]
        public decimal QA { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }
}