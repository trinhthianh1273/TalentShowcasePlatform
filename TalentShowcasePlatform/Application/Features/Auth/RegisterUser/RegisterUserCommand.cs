using Application.Features.Auth.Dto;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.RegisterUser;

public record RegisterUserCommand : IRequest<Result<AuthenticationResponseDto>>
{
	public string UserName { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }
	public string Bio { get; set; }
	public string AvatarUrl { get; set; }
	public Guid RoleId { get; set; }
}

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Result<AuthenticationResponseDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IConfiguration _configuration; // To access JWT settings
	private readonly IMapper _mapper;

	public RegisterUserHandler(IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_configuration = configuration;
		_mapper = mapper;
	}

	public async Task<Result<AuthenticationResponseDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
	{
		// 1. Check if user with email already exists
		var existingUser = await _unitOfWork.Repository<User>().Entities.FirstOrDefaultAsync(u => u.Email == request.Email);
		if (existingUser != null)
		{
			return Result<AuthenticationResponseDto>.Failure("Email already in use.");
		}

		// 2. Hash the password
		var passwordHasher = new PasswordHasher<User>();
		var hashedPassword = passwordHasher.HashPassword(null, request.Password);

		// 3. Create the User entity
		var user = new User
		{
			Id = Guid.NewGuid(),
			UserName = request.UserName,
			Email = request.Email,
			PasswordHash = hashedPassword,
			RoleId = request.RoleId,
			CreatedAt = DateTime.UtcNow // Use UTC for consistency
		};

		// 4. Add to database
		await _unitOfWork.Repository<User>().AddAsync(user);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult <= 0)
		{
			return Result<AuthenticationResponseDto>.Failure("Failed to create user.");
		}

		// 5. Generate JWT token
		var authResponse = GenerateJwtToken(user);

		return Result<AuthenticationResponseDto>.Success(authResponse, "User registered successfully.");
	}

	private AuthenticationResponseDto GenerateJwtToken(User user)
	{
		// 1. Define claims (data in the token)
		var claims = new List<Claim>
		{
			new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
			new Claim(ClaimTypes.Email, user.Email),
			new Claim(ClaimTypes.Role, user.RoleId.ToString()) // Or Role Name if needed
        };

		// 2. Get signing key from configuration
		var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
		var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

		// 3. Create the token
		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(claims),
			Expires = DateTime.UtcNow.AddHours(2), // Token expiration
			SigningCredentials = credentials,
			Issuer = _configuration["Jwt:Issuer"],
			Audience = _configuration["Jwt:Audience"]
		};

		var tokenHandler = new JwtSecurityTokenHandler();
		var token = tokenHandler.CreateToken(tokenDescriptor);

		return new AuthenticationResponseDto
		{
			UserId = user.Id,
			UserName = user.UserName,
			Email = user.Email,
			Token = tokenHandler.WriteToken(token)
		};
	}
}

