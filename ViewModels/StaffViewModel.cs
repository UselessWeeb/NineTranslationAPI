namespace ViewModels
{
    public class StaffViewModel
    {
        public List<UserViewModel> ProjectManagers { get; set; }
        public List<UserViewModel> Translators { get; set; }
        public List<UserViewModel> Editors { get; set; }
        public List<UserViewModel> UiTeam { get; set; }
        public List<UserViewModel> TechTeam { get; set; }
        public List<UserViewModel> QaTeam { get; set; }
    }
}