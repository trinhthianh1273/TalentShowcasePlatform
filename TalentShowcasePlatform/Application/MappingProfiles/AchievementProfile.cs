using Application.Features.Achievements.Command;
using Application.Features.Achievements.Query;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MappingProfiles;

public class AchievementProfile : Profile
{
	public AchievementProfile()
	{
		CreateMap<Achievement, AchievementDto>();
		//CreateMap<User, UserDto>(); // Ánh xạ cho User nếu bạn muốn bao gồm nó
		CreateMap<CreateAchievementCommand, Achievement>(); // Ánh xạ từ Command về Entity khi tạo
		CreateMap<UpdateAchievementCommand, Achievement>(); // Ánh xạ từ Command về Entity khi cập nhật
	}
}
