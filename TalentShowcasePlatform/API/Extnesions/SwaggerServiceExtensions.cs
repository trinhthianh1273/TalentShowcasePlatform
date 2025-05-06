using Microsoft.OpenApi.Models;

namespace API.Extnesions;

public static class SwaggerServiceExtensions
{
	public static IServiceCollection AddCustomOpenApi(this IServiceCollection services)
	{
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen(c =>
		{
			c.SwaggerDoc("v1", new OpenApiInfo
			{
				Title = "My Custom API",
				Version = "v1",
				Description = "An awesome API for testing and learning 🚀"
			});

			// Optional: thêm XML docs nếu bạn muốn
			// var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
			// var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
			// c.IncludeXmlComments(xmlPath);
		});

		return services;
		}
	}
