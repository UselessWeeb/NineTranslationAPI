using AutoMapper;
using Models;
using ViewModels;
using System.Globalization;

namespace MappingProfile
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            var vietnameseCulture = new CultureInfo("vi-VN");

            CreateMap<Project, ProjectViewModel>()
                .ForMember(dest => dest.Date, otp => otp.MapFrom(src => src.Date.ToString("dddd, dd/MM/yyyy", vietnameseCulture)))
                .ForMember(dest => dest.TranslationProgress, opt => opt.MapFrom(src => src.TranslationProgress))
                .ForMember(dest => dest.Detail, opt => opt.MapFrom(src => src.Detail));

            CreateMap<TranslationProgress, TranslationProgressViewModel>()
                .ForMember(dest => dest.LastUpdated, otp => otp.MapFrom(src => src.LastUpdated.ToString("dddd, dd/MM/yyyy", vietnameseCulture)));

            CreateMap<ProjectDetail, ProjectDetailViewModel>()
                .ForMember(dest => dest.ReleaseDate, otp => otp.MapFrom(src => src.ReleaseDate.ToString("dddd, dd/MM/yyyy", vietnameseCulture)));

            CreateMap<ProjectStaff, UserViewModel>();
            CreateMap<DownloadDetail, DownloadDetailViewModel>();
            CreateMap<PatchUpdate, PatchUpdateViewModel>()
                .ForMember(dest => dest.ReleaseDate, otp => otp.MapFrom(src => src.ReleaseDate.ToString("dddd, dd/MM/yyyy", vietnameseCulture)));
        }
    }
}
