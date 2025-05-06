using Application.Features.GroupMembers.Dto;
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

namespace Application.Features.GroupMembers.Query;

public record GetAllGroupMembersQuery : IRequest<Result<IEnumerable<GroupMemberDto>>>
{
}

public class GetAllGroupMembersQueryHandler : IRequestHandler<GetAllGroupMembersQuery, Result<IEnumerable<GroupMemberDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetAllGroupMembersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<GroupMemberDto>>> Handle(GetAllGroupMembersQuery request, CancellationToken cancellationToken)
	{
		var groupMembers = await _unitOfWork.Repository<GroupMember>()
			.GetAllAsync(include: q => q.Include(gm => gm.Group).Include(gm => gm.User));
		return Result<IEnumerable<GroupMemberDto>>.Success(_mapper.Map<IEnumerable<GroupMemberDto>>(groupMembers));
	}
}
