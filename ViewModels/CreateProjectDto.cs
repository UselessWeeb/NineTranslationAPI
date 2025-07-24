using Microsoft.AspNetCore.Http;

namespace ViewModels
{
    public class CreateProjectDto
    {
        public required string Finder { get; set; }
        public required string Heading { get; set; }
        public required string By { get; set; }
        public required IFormFile ThumbnailFile { get; set; }
        public required string Status { get; set; } = "In Progress";
        public required string ShortDescription { get; set; }
        public required DateTime Date { get; set; } = DateTime.UtcNow;
        public required string Link { get; set; }
        public required string Type { get; set; } = "project";
        public required bool IsCarousel { get; set; } = false;
        public CreateTranslationProgressDto? TranslationProgress { get; set; }
        public CreateProjectDetailDto? Detail { get; set; }
    }
}
