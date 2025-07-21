using Microsoft.AspNetCore.Http;

namespace ViewModels
{
    public class CreateProjectDto
    {
        public string Finder { get; set; }

        public string Heading { get; set; }

        public string By { get; set; }

        public IFormFile? ThumbnailFile { get; set; }

        public string Status { get; set; } = "In Progress";

        public string ShortDescription { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        public string Link { get; set; }

        public string Type { get; set; } = "project";
    }
}
