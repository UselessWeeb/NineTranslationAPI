using Microsoft.AspNetCore.Mvc;

namespace ViewModels
{
    public class CreateProjectDetailDto
    {
        public string? Publisher { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? Playtime { get; set; }
        public string? Genre { get; set; }
        public string? VndbLink { get; set; }
        public string? OfficialPage { get; set; }
        public string? FullDescription { get; set; }
        public CreateDownloadDetailDto? DownloadDetail { get; set; }
        public List<CreatePatchUpdateDto>? PatchHistory { get; set; }
        public string? PatchSize { get; set; }
        public string? DemoVideoUrl { get; set; }
        [FromForm]
        public List<CreateProjectStaffDto>? StaffRoles { get; set; }
    }
}