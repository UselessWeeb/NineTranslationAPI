using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Dto
{
    public class CreatePostDetailDto
    {
        public string Publisher { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Playtime { get; set; }
        public string Genre { get; set; }
        public string VndbLink { get; set; }
        public string OfficialPage { get; set; }
        public string FullDescription { get; set; }
        public string PatchSize { get; set; }
        public string DemoVideoUrl { get; set; }
    }
}
