using Application.Features.Users.Command;
using Application.Features.Users.Dto;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MappingProfiles;

public class UserProfile : Profile
{
	public UserProfile()
	{
		CreateMap<User, UserDto>()
			.ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name)); ;
		CreateMap<CreateUserCommand, User>();
		CreateMap<UpdateUserCommand, User>();

		// Mapping từ User sang UserNavigationDto
		CreateMap<User, UserNavigationDto>()
			.ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role)); // Nếu bạn muốn map Role

	}
}