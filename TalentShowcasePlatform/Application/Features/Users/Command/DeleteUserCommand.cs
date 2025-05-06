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

public class DeleteUserCommand : IRequest<Result<bool>>
{
	public Guid Id { get; set; }
}

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteUserHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
	{
		var user = await _unitOfWork.Repository<User>().GetByIdAsync(request.Id);

		if (user == null)
		{
			return Result<bool>.Failure("User not found.");
		}

		await _unitOfWork.Repository<User>().DeleteAsync(user);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<bool>.Success(true);
		}
		else
		{
			return Result<bool>.Failure("Failed to delete user.");
		}
	}
}


