using Application.Features.Roles.Dto;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Roles.Query;

public class GetRoleByIdQuery : IRequest<Result<RoleDto>>
{
	public Guid Id { get; set; }
}

public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, Result<RoleDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetRoleByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<RoleDto>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
	{
		var role = await _unitOfWork.Repository<Role>().GetByIdAsync(request.Id);

		if (role == null)
		{
			return Result<RoleDto>.Failure("Role not found.");
		}

		return Result<RoleDto>.Success(_mapper.Map<RoleDto>(role));
	}
}


