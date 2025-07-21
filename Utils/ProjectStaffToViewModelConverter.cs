using AutoMapper;
using Models;
using Models.Enums;
using ViewModels;

namespace Utils
{
    public class ProjectStaffToViewModelConverter
        : ITypeConverter<IEnumerable<ProjectStaff>, StaffDto>
    {
        public ProjectStaffToViewModelConverter() { }

        public StaffDto Convert(IEnumerable<ProjectStaff> source, StaffDto destination, ResolutionContext context)
        {
            return new StaffDto
            {
                ProjectManagers = source.Where(ps => ps.Role == StaffRoleType.ProjectManager).Select(ps => ps.User.DisplayName).ToList(),
                Translators = source.Where(ps => ps.Role == StaffRoleType.Translator).Select(ps => ps.User.DisplayName).ToList(),
                Editors = source.Where(ps => ps.Role == StaffRoleType.Editor).Select(ps => ps.User.DisplayName).ToList(),
                UiTeam = source.Where(ps => ps.Role == StaffRoleType.UI).Select(ps => ps.User.DisplayName).ToList(),
                TechTeam = source.Where(ps => ps.Role == StaffRoleType.Tech).Select(ps => ps.User.DisplayName).ToList(),
                QaTeam = source.Where(ps => ps.Role == StaffRoleType.QA).Select(ps => ps.User.DisplayName).ToList()
            };
        }
    }
}