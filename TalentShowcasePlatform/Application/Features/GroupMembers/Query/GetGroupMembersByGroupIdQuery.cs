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

public record GetGroupMembersByGroupIdQuery : IRequest<Result<IEnumerable<GroupMemberDto>>>
{
	public Guid GroupId { get; set; }
}

public class GetGroupMembersByGroupIdQueryHandler : IRequestHandler<GetGroupMembersByGroupIdQuery, Result<IEnumerable<GroupMemberDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetGroupMembersByGroupIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<GroupMemberDto>>> Handle(GetGroupMembersByGroupIdQuery request, CancellationToken cancellationToken)
	{
		var groupMembers = _unitOfWork.Repository<GroupMember>().Entities
							.Where(gm => gm.GroupId == request.GroupId)
							.Include(gm => gm.Group)
							.Include(gm => gm.User);
		return Result<IEnumerable<GroupMemberDto>>.Success(_mapper.Map<IEnumerable<GroupMemberDto>>(groupMembers));
	}
}
