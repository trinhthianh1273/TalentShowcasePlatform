using Application.Features.GroupMembers.Dto;
using Application.Features.Groups.Dto;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Features.GroupMembers.Command;

public record AddListUserToGroupCommand : IRequest<Result<List<GroupMemberDto>>>
{
	public List<AddUserToGroupCommand> Users { get; set; }
}

public class AddListUserToGroupCommandHandler : IRequestHandler<AddListUserToGroupCommand, Result<List<GroupMemberDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public AddListUserToGroupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<List<GroupMemberDto>>> Handle(AddListUserToGroupCommand request, CancellationToken cancellationToken)
	{
		var groupMembers = new List<GroupMember>();
		foreach (var item in request.Users)
		{
			var groupMember = new GroupMember
			{
				Id = Guid.NewGuid(),
				GroupId = item.GroupId,
				UserId = item.UserId,
				JoinedAt = DateTime.UtcNow
			};
			var existingMember = await _unitOfWork.Repository<GroupMember>()
			.FindAsync(gm => gm.GroupId == item.GroupId && gm.UserId == item.UserId);
			if (!existingMember.Any())
			{
				groupMembers.Add(groupMember);
			}
		}
		// Check if the user is already in the group
		
		

		await _unitOfWork.Repository<GroupMember>().AddRangeAsync(groupMembers);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			var resultDto = _mapper.Map<List<GroupMemberDto>>(groupMembers);
			return Result<List<GroupMemberDto>>.Success(resultDto, "Người dùng tham gia thành công");
		}
		else
		{
			return Result<List<GroupMemberDto>>.Failure("Không thể thêm người dùng vào nhóm.");
		}
	}
}
