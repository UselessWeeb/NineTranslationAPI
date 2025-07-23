using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Dto
{
    public class CreatePostDto
    {
        public string Finder { get; set; }
        public string Heading { get; set; }
        public string By { get; set; }
        public IFormFile? ThumbnailFile { get; set; }
        public string Status { get; set; } = "In Progress";
        public string ShortDescription { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string Link { get; set; }
        public string Type { get; set; } = "post";
        public CreatePostDetailDto? Detail { get; set; }
    }
}
