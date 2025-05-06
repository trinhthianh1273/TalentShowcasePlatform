using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Roles.Command;

public class DeleteRoleCommand : IRequest<Result<bool>>
{
	public Guid Id { get; set; }
}


public class DeleteRoleHandler : IRequestHandler<DeleteRoleCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteRoleHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<bool>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
	{
		var role = await _unitOfWork.Repository<Role>().GetByIdAsync(request.Id);

		if (role == null)
		{
			return Result<bool>.Failure("Role not found.");
		}

		await _unitOfWork.Repository<Role>().DeleteAsync(role);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<bool>.Success(true);
		}
		else
		{
			return Result<bool>.Failure("Failed to delete role.");
		}
	}
}



