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

public class UpdateRoleCommand : IRequest<Result<bool>>
{
	public Guid Id { get; set; }
	public string Name { get; set; }
}

public class UpdateRoleHandler : IRequestHandler<UpdateRoleCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public UpdateRoleHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<bool>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
	{
		var role = await _unitOfWork.Repository<Role>().GetByIdAsync(request.Id);

		if (role == null)
		{
			return Result<bool>.Failure("Role not found.");
		}

		role.Name = request.Name;

		await _unitOfWork.Repository<Role>().UpdateAsync(role);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<bool>.Success(true);
		}
		else
		{
			return Result<bool>.Failure("Failed to update role.");
		}
	}
}



