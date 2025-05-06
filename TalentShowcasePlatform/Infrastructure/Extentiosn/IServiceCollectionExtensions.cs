using Domain.Common;
using Domain.Interfaces;
using Infrastructure.Persistences.BEContext;
using Infrastructure.Persistences.Interceptors;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extentiosn;

public static class IServiceCollectionExtensions
{
	public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		//services.AddDbContext<BEContext>();

		services.AddRepositories();
		services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
		services.AddScoped<PublishDomainEventsInterceptor>();
		//services.AddScoped<IPublisher, IMediator>();
	}

	private static void AddRepositories(this IServiceCollection services)
	{
		services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
		services.AddTransient<IUnitOfWork, UnitOfWork>();
	}
}
