using AutoMapper;
using Models;
using ViewModels;

namespace MappingProfile
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectViewModel>()
                .ForMember(dest => dest.TranslationProgress, opt => opt.MapFrom(src => src.TranslationProgress))
                .ForMember(dest => dest.Detail, opt => opt.MapFrom(src => src.Detail));

            CreateMap<TranslationProgress, TranslationProgressViewModel>();
            CreateMap<ProjectDetail, ProjectDetailViewModel>();
            CreateMap<ProjectStaff, UserViewModel>();
            CreateMap<DownloadDetail, DownloadDetailViewModel>();
            CreateMap<PatchUpdate, PatchUpdateViewModel>();
        }
    }
}
