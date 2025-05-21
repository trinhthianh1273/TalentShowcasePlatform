using Application.Features.Groups.Dto;
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

public record GetJoinedGroupIdsByUserQuery : IRequest<Result<List<Guid>>>
{
	public Guid UserId { get; set; }

	public GetJoinedGroupIdsByUserQuery(Guid userId)
	{
		UserId = userId;
	}
}

public class GetJoinedGroupIdsByUserQueryHandler : IRequestHandler<GetJoinedGroupIdsByUserQuery, Result<List<Guid>>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetJoinedGroupIdsByUserQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<List<Guid>>> Handle(GetJoinedGroupIdsByUserQuery request, CancellationToken cancellationToken)
	{
		var groupIds = await _unitOfWork.Repository<GroupMember>().Entities
										.Where(i => i.UserId == request.UserId)
										.Select(g => g.GroupId)
										.Distinct()
										.ToListAsync(cancellationToken);

		return Result<List<Guid>>.Success(groupIds);
	}
}

