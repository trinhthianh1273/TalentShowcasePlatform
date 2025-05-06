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

public class UpdateUserTalentCommand : IRequest<Result<bool>>
{
	public Guid Id { get; set; }
	public Guid UserId { get; set; }
	public Guid CategoryId { get; set; }
}

public class UpdateUserTalentHandler : IRequestHandler<UpdateUserTalentCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public UpdateUserTalentHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<bool>> Handle(UpdateUserTalentCommand request, CancellationToken cancellationToken)
	{
		var userTalent = await _unitOfWork.Repository<UserTalent>().GetByIdAsync(request.Id);

		if (userTalent == null)
		{
			return Result<bool>.Failure("User talent not found.");
		}

		userTalent.UserId = request.UserId;
		userTalent.CategoryId = request.CategoryId;

		await _unitOfWork.Repository<UserTalent>().UpdateAsync(userTalent);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<bool>.Success(true);
		}
		else
		{
			return Result<bool>.Failure("Failed to update user talent.");
		}
	}
}



