using Application.Features.Groups.Command;
using Application.Features.Groups.Dto;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MappingProfiles;

public class GroupProfile : Profile
{
	public GroupProfile()
	{
		CreateMap<Group, GroupDto>();
		CreateMap<CreateGroupCommand, Group>();
		CreateMap<UpdateGroupCommand, Group>();
	}
}
