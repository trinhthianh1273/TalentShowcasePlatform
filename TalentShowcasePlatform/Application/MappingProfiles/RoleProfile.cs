using Application.Features.Roles.Command;
using Application.Features.Roles.Dto;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MappingProfiles;

public class RoleProfile : Profile
{
	public RoleProfile()
	{
		CreateMap<Role, RoleDto>();
		CreateMap<CreateRoleCommand, Role>();
		CreateMap<UpdateRoleCommand, Role>();
	}
}
