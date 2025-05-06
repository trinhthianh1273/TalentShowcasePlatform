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

// Application Layer - Commands
public class CreateRoleCommand : IRequest<Result<Guid>>
{
	public string Name { get; set; }
}

public class CreateRoleHandler : IRequestHandler<CreateRoleCommand, Result<Guid>>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreateRoleHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<Guid>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
	{
		var role = new Role
		{
			Id = Guid.NewGuid(),
			Name = request.Name
		};

		await _unitOfWork.Repository<Role>().AddAsync(role);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<Guid>.Success(role.Id);
		}
		else
		{
			return Result<Guid>.Failure("Failed to create role.");
		}
	}
}



