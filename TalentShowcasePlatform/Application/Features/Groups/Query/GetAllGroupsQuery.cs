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

public record GetAllGroupsQuery : IRequest<Result<IEnumerable<GroupDto>>>
{
}

public class GetAllGroupsQueryHandler : IRequestHandler<GetAllGroupsQuery, Result<IEnumerable<GroupDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetAllGroupsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<GroupDto>>> Handle(GetAllGroupsQuery request, CancellationToken cancellationToken)
	{
		var groups = await _unitOfWork.Repository<Group>()
			.GetAllAsync(include: q => q.Include(g => g.CreatedByUser));
		return Result<IEnumerable<GroupDto>>.Success(_mapper.Map<IEnumerable<GroupDto>>(groups));
	}
}
