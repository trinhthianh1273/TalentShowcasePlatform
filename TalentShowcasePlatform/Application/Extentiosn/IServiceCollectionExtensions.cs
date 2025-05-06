using Application.MappingProfiles;
using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extentiosn;

public static class IServiceCollectionExtensions
{
	public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
	{
		// Đăng ký PasswordHasher cho kiểu User của bạn
		services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
		//services.AddAutoMapper(typeof(AchievementProfile).Assembly); // Đăng ký profile của Achievement
		// Nếu bạn có nhiều profile trong các assembly khác nhau, bạn có thể đăng ký toàn bộ assembly:
		services.AddAutoMapper(Assembly.GetExecutingAssembly());
		//services.AddAutoMapper(typeof(AwardProfile).Assembly);
	}
}
