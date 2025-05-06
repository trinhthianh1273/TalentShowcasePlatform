using Application.Features.Contests.Command;
using Application.Features.Contests.Dto;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MappingProfiles;

public class ContestProfile : Profile
{
	public ContestProfile()
	{
		CreateMap<Contest, ContestDto>();
		//CreateMap<User, UserDto>(); // If you need to map CreatedByUser
		CreateMap<CreateContestCommand, Contest>();
		CreateMap<UpdateContestCommand, Contest>();
	}
}


