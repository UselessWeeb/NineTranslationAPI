using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Dto
{
    public class UpdateProjectDto
    {
        public int Id { get; set; }
        public string? Finder { get; set; }
        public string? Heading { get; set; }
        public string? By { get; set; }
        public IFormFile? ThumbnailFile { get; set; }
        public string Status { get; set; } = "In Progress";
        public string? ShortDescription { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string? Link { get; set; }
        public string Type { get; set; } = "project";
        public bool IsCarousel { get; set; } = false;
        public CreateTranslationProgressDto? TranslationProgress { get; set; }
        public CreateProjectDetailDto? Detail { get; set; }
    }
}
