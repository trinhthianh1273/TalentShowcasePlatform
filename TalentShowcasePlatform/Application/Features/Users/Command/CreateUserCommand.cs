using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Command;

public class CreateUserCommand : IRequest<Result<Guid>>
{
	public string UserName { get; set; }
	public string Email { get; set; }
	public string FullName { get; set; }
	public string Password { get; set; } // Consider hashing here or in handler
	public string Bio { get; set; }
	public string AvatarUrl { get; set; }
	public string Location { get; set; }
	public Guid RoleId { get; set; }
}

public class CreateUserHandler : IRequestHandler<CreateUserCommand, Result<Guid>>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreateUserHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
	{
		var passwordHasher = new PasswordHasher<User>();
		var hashedPassword = passwordHasher.HashPassword(null, request.Password);

		var user = new User
		{
			Id = Guid.NewGuid(),
			UserName = request.UserName,
			Email = request.Email,
			FullName = request.FullName,
			PasswordHash = hashedPassword, // **NEVER** store plain text passwords
			Bio = request.Bio,
			AvatarUrl = request.AvatarUrl,
			Location = request.Location,
			RoleId = request.RoleId
		};

		await _unitOfWork.Repository<User>().AddAsync(user);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<Guid>.Success(user.Id);
		}
		else
		{
			return Result<Guid>.Failure("Failed to create user.");
		}
	}
}

