﻿using Application.Features.Jobs.Command;
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
			.ForMember(dest => dest.PostedByUser, opt => opt.MapFrom(src => src.PostedByUser)); // Nếu bạn muốn map Role;
		CreateMap<CreateJobCommand, Job>();
		CreateMap<UpdateJobCommand, Job>();
	}
}
