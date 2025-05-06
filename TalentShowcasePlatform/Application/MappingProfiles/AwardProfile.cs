using Application.Features.Awards.Command;
using Application.Features.Awards.Query;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MappingProfiles;

public class AwardProfile : Profile
{
	public AwardProfile()
	{
		CreateMap<Award, AwardDto>();
		//CreateMap<User, UserDto>(); // Map User if needed
		CreateMap<CreateAwardCommand, Award>();
		CreateMap<UpdateAwardCommand, Award>();
	}
}
