using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class ProjectDetail
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string Publisher { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [StringLength(50)]
        public string Playtime { get; set; }

        [StringLength(100)]
        public string Genre { get; set; }

        [Url]
        [StringLength(255)]
        public string VndbLink { get; set; }

        [Url]
        [StringLength(255)]
        public string OfficialPage { get; set; }

        public string FullDescription { get; set; }

        // Foreign keys
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public virtual DownloadDetail DownloadDetail { get; set; }
        public virtual List<PatchUpdate> PatchHistory { get; set; }

        [StringLength(50)]
        public string PatchSize { get; set; }

        [StringLength(100)]
        public string DemoVideoUrl { get; set; }
        public virtual ICollection<ProjectStaff>? StaffRoles { get; set; }
    }
}