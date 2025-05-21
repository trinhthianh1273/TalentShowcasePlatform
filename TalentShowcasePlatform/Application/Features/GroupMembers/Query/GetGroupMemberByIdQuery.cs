
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

public class GetGroupMemberByIdQuery : IRequest<Result<GroupMemberDto>>
{
	public Guid Id { get; set; }
}

public class GetGroupMemberByIdQueryHandler : IRequestHandler<GetGroupMemberByIdQuery, Result<GroupMemberDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetGroupMemberByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<GroupMemberDto>> Handle(GetGroupMemberByIdQuery request, CancellationToken cancellationToken)
	{
		var groupMember = await _unitOfWork.Repository<GroupMember>()
									.Entities
									.Where(i => i.Id == request.Id)
									.Include(i => i.Group)
									.Include(i => i.User)
									.FirstOrDefaultAsync(cancellationToken);

		if (groupMember == null)
		{
			return Result<GroupMemberDto>.Failure("Không tìm thấy thành viên nhóm.");
		}

		return Result<GroupMemberDto>.Success(_mapper.Map<GroupMemberDto>(groupMember));
	}
}

