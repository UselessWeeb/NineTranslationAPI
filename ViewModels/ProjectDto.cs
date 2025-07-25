using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Finder { get; set; }
        public string? Heading { get; set; }
        public string? By { get; set; }
        public string? Thumbnail { get; set; }
        public string? Status { get; set; }
        public string? ShortDescription { get; set; }
        public string? Date { get; set; }
        public string? Link { get; set; }
        public string? Type { get; set; }
        public bool isCarousel { get; set; } = false;
        public bool isActive { get; set; } = true;
        public TranslationProgressDto? TranslationProgress { get; set; }
        public ProjectDetailDto? Detail { get; set; }
    }
}
