using Application.Features.Groups.Dto;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Groups.Query;

public record GetGroupsCreatedByUserQuery : IRequest<Result<IEnumerable<GroupDto>>>
{
	public Guid CreatedBy { get; set; }
}

public class GetGroupsCreatedByUserQueryHandler : IRequestHandler<GetGroupsCreatedByUserQuery, Result<IEnumerable<GroupDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetGroupsCreatedByUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<GroupDto>>> Handle(GetGroupsCreatedByUserQuery request, CancellationToken cancellationToken)
	{
		var groups = _unitOfWork.Repository<Group>().Entities
						.Where(g => g.CreatedBy == request.CreatedBy)
						.Include(g => g.CreatedByUser);
		return Result<IEnumerable<GroupDto>>.Success(_mapper.Map<IEnumerable<GroupDto>>(groups));
	}
}

