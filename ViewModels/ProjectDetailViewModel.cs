namespace ViewModels
{
    public class ProjectDetailViewModel
    {
        public string Publisher { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Playtime { get; set; }
        public string Genre { get; set; }
        public string VndbLink { get; set; }
        public string OfficialPage { get; set; }
        public string FullDescription { get; set; }
        public StaffViewModel Staff { get; set; }
        public DownloadDetailViewModel DownloadDetail { get; set; }
        public List<PatchUpdateViewModel> PatchHistory { get; set; }
        public string PatchSize { get; set; }
        public string DemoVideoUrl { get; set; }
    }
}