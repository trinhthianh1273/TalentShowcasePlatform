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

public class GetAllRolesQuery : IRequest<Result<IEnumerable<RoleDto>>>
{
}

public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, Result<IEnumerable<RoleDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetAllRolesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<RoleDto>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
	{
		var roles = await _unitOfWork.Repository<Role>().GetAllAsync();

		return Result<IEnumerable<RoleDto>>.Success(_mapper.Map<IEnumerable<RoleDto>>(roles));
	}
}



