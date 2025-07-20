using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class DownloadDetail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string PatchVersion { get; set; }

        [Url]
        [StringLength(255)]
        public string OfficialLink { get; set; }

        [Url]
        [StringLength(255)]
        public string Download1 { get; set; }

        [Url]
        [StringLength(255)]
        public string Download2 { get; set; }

        [Url]
        [StringLength(255)]
        public string Download3 { get; set; }

        [Url]
        [StringLength(255)]
        public string TutorialVideoLink { get; set; }

        // Foreign key
        public int ProjectDetailId { get; set; }
        public ProjectDetail ProjectDetail { get; set; }
    }
}