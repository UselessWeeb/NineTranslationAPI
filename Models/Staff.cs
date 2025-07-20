namespace Models
{
    public class Staff
    {
        public int Id { get; set; }
        public List<User> ProjectManagers { get; set; }
        public List<User> Translators { get; set; }
        public List<User> Editors { get; set; }
        public List<User> UiTeam { get; set; }
        public List<User> TechTeam { get; set; }
        public List<User> QaTeam { get; set; }
    }
}