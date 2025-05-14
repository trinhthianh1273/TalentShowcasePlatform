using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Command;

public class UpdateUserCommand : IRequest<Result<bool>>
{
	public Guid Id { get; set; }
	public string UserName { get; set; }
	public string FullName { get; set; }
	public string Email { get; set; }
	public string Bio { get; set; }
	public string Location { get; set; }
}

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public UpdateUserHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<bool>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
	{
		var user = await _unitOfWork.Repository<User>().GetByIdAsync(request.Id);

		if (user == null)
		{
			return Result<bool>.Failure("User not found.");
		}

		user.UserName = request.UserName;
		user.Email = request.Email;
		user.Bio = request.Bio;
		user.FullName = request.FullName;
		user.Location = request.Location;

		await _unitOfWork.Repository<User>().UpdateAsync(user);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<bool>.Success(true);
		}
		else
		{
			return Result<bool>.Failure("Failed to update user.");
		}
	}
}



