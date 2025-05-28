
using API.Extnesions;
using Application;
using Application.Extentiosn;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Extentiosn;
using Infrastructure.Persistences.BEContext;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplication(builder.Configuration); // Đăng ký Application Layer

builder.Services.AddMediatR(cfg =>
{
	cfg.RegisterServicesFromAssembly(typeof(AssemblyMarker).Assembly); // hoặc Application Layer
});
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddDbContext<BEContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DbContext")));
builder.Services.AddScoped<IActivityEventPublisher, ActivityEventPublisher>();

builder.Services.AddSingleton<IFileService, FileService>(); // Đăng ký FileService

builder.Services.AddSignalR(); // Thêm SignalR

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi() ;
builder.Services.AddSwaggerGen();  // ✅ Bổ sung bắt buộc để tránh lỗi ISwaggerProvider

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = builder.Configuration["Jwt:Issuer"],
			ValidAudience = builder.Configuration["Jwt:Audience"],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
		};
	});

builder.Services.AddAuthorization(); // If you need authorization policies

builder.Services.AddCors(options =>
{
	//options.AddDefaultPolicy(builder =>
	//{
	//	builder.WithOrigins("http://localhost:4200/")  // Thay bằng domain của client
	//		   .AllowAnyHeader()
	//		   .AllowAnyMethod()
	//		   .AllowCredentials(); // Quan trọng cho SignalR
	//});

	options.AddPolicy("MyPolicy", builder =>
	{
		builder.AllowAnyOrigin()
			.AllowAnyMethod()
			.AllowAnyHeader();
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	// 👇 THÊM HAI DÒNG NÀY để bật Swagger thủ công
	app.UseSwagger();
	app.UseSwaggerUI(options =>
	{
		options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
		options.RoutePrefix = "swagger"; // 👈 Truy cập Swagger UI ở http://localhost:5000/
	});
	app.MapOpenApi();
}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors();

app.UseRouting();

// Cho phép truy cập static files
//app.UseStaticFiles();
//app.UseStaticFiles(new StaticFileOptions
//{
//	FileProvider = new PhysicalFileProvider(
//		Path.Combine(builder.Environment.WebRootPath, "Assets/Video")),
//	RequestPath = "/Assets/Video"
//});

// ...
   //app.UseEndpoints(endpoints =>
   //{
   //	endpoints.MapHub<ChatHub>("/chat"); // Map Hub tới một endpoint
   //	endpoints.MapControllers();
   //});
app.UseCors("MyPolicy");
app.MapControllers();

app.Run();
