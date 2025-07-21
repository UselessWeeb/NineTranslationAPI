namespace Models
{
    public class Staff
    {
        public int Id { get; set; }
        public virtual List<User> ProjectManagers { get; set; }
        public virtual List<User> Translators { get; set; }
        public virtual List<User> Editors { get; set; }
        public virtual List<User> UiTeam { get; set; }
        public virtual List<User> TechTeam { get; set; }
        public virtual List<User> QaTeam { get; set; }
    }
}