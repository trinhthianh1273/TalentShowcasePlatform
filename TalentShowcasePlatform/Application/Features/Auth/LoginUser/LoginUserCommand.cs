using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using Application.Features.Auth.Dto;
using AutoMapper;
using Shared.Results;

namespace Application.Features.Auth.LoginUser;

public class LoginUserCommand : IRequest<Result<AuthenticationResponseDto>> // Trả về JWT token
{
	public string Email { get; set; }
	public string Password { get; set; }
}

public class LoginUserHandler : IRequestHandler<LoginUserCommand, Result<AuthenticationResponseDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IConfiguration _configuration;
	private readonly IMapper _mapper;
	private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

	public LoginUserHandler(IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_configuration = configuration;
		_mapper = mapper;
	}

	public async Task<Result<AuthenticationResponseDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
	{
		// 1. Find the user by email
		var user = await _unitOfWork.Repository<User>().Entities.FirstOrDefaultAsync(u => u.Email == request.Email);

		if (user == null)
		{
			return Result<AuthenticationResponseDto>.Failure("Invalid email or password."); // Unauthorized
		}

		// 2. Verify the password
		var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(null, user.PasswordHash, request.Password);

		if (passwordVerificationResult == PasswordVerificationResult.Failed)
		{
			return Result<AuthenticationResponseDto>.Failure("Invalid email or password.");
		}

		// 3. Generate JWT token
		var authResponse = GenerateJwtToken(user);

		return Result<AuthenticationResponseDto>.Success(authResponse, "Login successful.");
	}

	private AuthenticationResponseDto GenerateJwtToken(User user)
	{
		// (Same JWT generation logic as in RegisterUserHandler)
		var claims = new List<Claim>
		{
			new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
			new Claim(ClaimTypes.Email, user.Email),
			new Claim(ClaimTypes.Role, user.RoleId.ToString())
		};

		var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
		var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(claims),
			Expires = DateTime.UtcNow.AddHours(2),
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