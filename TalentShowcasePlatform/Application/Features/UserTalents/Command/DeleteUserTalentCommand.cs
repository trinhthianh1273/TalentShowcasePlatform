using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserTalents.Command;

public class DeleteUserTalentCommand : IRequest<Result<bool>>
{
	public Guid Id { get; set; }
}

public class DeleteUserTalentHandler : IRequestHandler<DeleteUserTalentCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteUserTalentHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<bool>> Handle(DeleteUserTalentCommand request, CancellationToken cancellationToken)
	{
		var userTalent = await _unitOfWork.Repository<UserTalent>().GetByIdAsync(request.Id);

		if (userTalent == null)
		{
			return Result<bool>.Failure("User talent not found.");
		}

		await _unitOfWork.Repository<UserTalent>().DeleteAsync(userTalent);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<bool>.Success(true);
		}
		else
		{
			return Result<bool>.Failure("Failed to delete user talent.");
		}
	}
}


