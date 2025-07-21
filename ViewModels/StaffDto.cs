namespace ViewModels
{
    public class StaffDto
    {
        public List<string> ProjectManagers { get; set; }
        public List<string> Translators { get; set; }
        public List<string> Editors { get; set; }
        public List<string> UiTeam { get; set; }
        public List<string> TechTeam { get; set; }
        public List<string> QaTeam { get; set; }
    }
}