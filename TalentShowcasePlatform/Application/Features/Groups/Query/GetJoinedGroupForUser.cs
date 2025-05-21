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

public record GetJoinedGroupForUser : IRequest<Result<IEnumerable<GroupDto>>>
{
	public Guid userId { get; set; }

	public GetJoinedGroupForUser(Guid userId)
	{
		this.userId = userId;
	}
}

internal class GetJoinedGroupForUserHandler  : IRequestHandler<GetJoinedGroupForUser, Result<IEnumerable<GroupDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetJoinedGroupForUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<GroupDto>>> Handle(GetJoinedGroupForUser request, CancellationToken cancellationToken)
	{
		// Lấy danh sách groupId mà user là thành viên
		var groupIds = await _unitOfWork.Repository<GroupMember>().Entities
			.Where(gm => gm.UserId == request.userId)
			.Select(gm => gm.GroupId)
			.ToListAsync(cancellationToken);

		// Truy vấn danh sách group theo các groupId đã lấy
		var groups = await _unitOfWork.Repository<Group>().Entities
			.Where(g => groupIds.Contains(g.Id))
			.Include(g => g.CreatedByUser) // nếu cần thêm thông tin người tạo
			.Include(g => g.Category) // nếu cần thêm thông tin danh mục
			.ToListAsync(cancellationToken);

		return Result<IEnumerable<GroupDto>>.Success(_mapper.Map<IEnumerable<GroupDto>>(groups));
	}
}

