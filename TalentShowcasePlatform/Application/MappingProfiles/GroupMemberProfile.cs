using Application.Features.GroupMembers.Command;
using Application.Features.GroupMembers.Dto;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MappingProfiles;

public class GroupMemberProfile : Profile
{
	public GroupMemberProfile()
	{
		CreateMap<GroupMember, GroupMemberDto>();
		// You can add mappings for Group and User DTOs if you intend to include them
		// CreateMap<Group, GroupDto>();
		// CreateMap<User, UserDto>();
		CreateMap<AddUserToGroupCommand, GroupMember>();
	}
}
