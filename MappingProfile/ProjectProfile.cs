﻿using AutoMapper;
using Dto;
using Models;
using System.Globalization;
using Utils;
using ViewModels;

namespace MappingProfile
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            var vietnameseCulture = new CultureInfo("vi-VN");

            CreateMap<Project, ProjectDto>()
                .ForMember(dest => dest.Date, otp => otp.MapFrom(src => src.Date.ToString("dddd, dd/MM/yyyy", vietnameseCulture)))
                .ForMember(dest => dest.TranslationProgress, opt => opt.MapFrom(src => src.TranslationProgress))
                .ForMember(dest => dest.Detail, opt => opt.MapFrom(src => src.Detail));

            CreateMap<TranslationProgress, TranslationProgressDto>()
                .ForMember(dest => dest.LastUpdated, otp => otp.MapFrom(src => src.LastUpdated.ToString("dddd, dd/MM/yyyy", vietnameseCulture)));

            CreateMap<ProjectDetail, ProjectDetailDto>()
                .ForMember(dest => dest.LinkedStaff, opt => opt.MapFrom(src => src.StaffRoles))
                .ForMember(dest => dest.ReleaseDate, otp => otp.MapFrom(src => src.ReleaseDate.ToString("dddd, dd/MM/yyyy", vietnameseCulture)))
                .ForMember(dest => dest.Staff, opt => opt.MapFrom(src => src.StaffRoles));

            CreateMap<IEnumerable<ProjectStaff>, StaffDto>()
                .ConvertUsing<ProjectStaffToViewModelConverter>();

            CreateMap<ProjectStaff, ProjectStaffDto>();

            CreateMap<DownloadDetail, DownloadDetailDto>();
            CreateMap<PatchUpdate, PatchUpdateDto>()
                .ForMember(dest => dest.ReleaseDate, otp => otp.MapFrom(src => src.ReleaseDate.ToString("dddd, dd/MM/yyyy", vietnameseCulture)));

            CreateMap<CreateProjectDto, Project>();
            CreateMap<CreateTranslationProgressDto, TranslationProgress>();
            CreateMap<CreateProjectDetailDto, ProjectDetail>();
            CreateMap<CreateProjectStaffDto, ProjectStaff>();
            CreateMap<CreateDownloadDetailDto, DownloadDetail>();
            CreateMap<CreatePatchUpdateDto, PatchUpdate>();

            CreateMap<UpdateProjectDto, Project>();
            CreateMap<UpdatePatchUpdateDto, PatchUpdate>();
            CreateMap<UpdateProjectStaffDto, ProjectStaff>();
        }
    }
}
