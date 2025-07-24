namespace ViewModels
{
    public class CreatePatchUpdateDto
    {
        public string? Version { get; set; }
        public string? Detail { get; set; }
        public DateTime ReleaseDate { get; set; } = DateTime.UtcNow;
        public int ProjectDetailId { get; set; }
    }
}