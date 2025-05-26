using Application.Features.Jobs.Command;
using Application.Features.Jobs.Dto;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MappingProfiles;

public class JobProfile : Profile
{
	public JobProfile()
	{
		CreateMap<Job, JobDto>()
			.ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
			.ForMember(dest => dest.PostedByUser, opt => opt.MapFrom(src => src.PostedByUser)); // Nếu bạn muốn map Role;
		CreateMap<Job, JobShortDto>()
			.ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
			.ForMember(dest => dest.UserAvatarUrl, opt => opt.MapFrom(src => src.PostedByUser.AvatarUrl))
			.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.PostedByUser.UserName)); // Nếu bạn muốn map Role;
		
		CreateMap<CreateJobCommand, Job>();
		CreateMap<UpdateJobCommand, Job>();
	}
}
