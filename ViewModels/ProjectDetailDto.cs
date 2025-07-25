using Dto;

namespace ViewModels
{
    public class ProjectDetailDto
    {
        public string Publisher { get; set; }
        public string ReleaseDate { get; set; }
        public string Playtime { get; set; }
        public string Genre { get; set; }
        public string VndbLink { get; set; }
        public string OfficialPage { get; set; }
        public string FullDescription { get; set; }
        public StaffDto? Staff { get; set; }
        public ICollection<ProjectStaffDto>? LinkedStaff { get; set; }
        public DownloadDetailDto DownloadDetail { get; set; }
        public List<PatchUpdateDto> PatchHistory { get; set; }
        public string PatchSize { get; set; }
        public string DemoVideoUrl { get; set; }
    }
}