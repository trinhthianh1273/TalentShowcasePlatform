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
		CreateMap<Group, GroupDto>()
			.ForMember(dest => dest.CreatedByUserName, opt => opt.MapFrom(src => src.CreatedByUser.FullName))
			.ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
		CreateMap<CreateGroupCommand, Group>();
		CreateMap<UpdateGroupCommand, Group>();
	}
}
