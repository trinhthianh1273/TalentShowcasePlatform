using Application.Features.UserTalents.Command;
using Application.Features.UserTalents.Dto;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MappingProfiles;

public class UserTalentProfile : Profile
{
	public UserTalentProfile()
	{
		CreateMap<UserTalent, UserTalentDto>();
		CreateMap<CreateUserTalentCommand, UserTalent>();
		CreateMap<UpdateUserTalentCommand, UserTalent>();
	}
}