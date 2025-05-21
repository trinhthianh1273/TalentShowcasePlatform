using Application.Features.GroupPostComments.Dto;
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

namespace Application.Features.GroupPostComments.Query;

public record GetCommentPostByIdQuery : IRequest<Result<GroupPostCommentDto>>
{
	public Guid Id { get; set; }
}

internal class GetCommentByIdQueryHandler : IRequestHandler<GetCommentPostByIdQuery, Result<GroupPostCommentDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetCommentByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<GroupPostCommentDto>> Handle(GetCommentPostByIdQuery request, CancellationToken cancellationToken)
	{
		var result = await _unitOfWork.Repository<CommentGroupPost>()
										.GetByIdAsync(request.Id, include: q => q.Include(g => g.User).Include(g => g.ChildComments));

		if (result == null)
		{
			return Result<GroupPostCommentDto>.Failure("Not Found.");
		}

		return Result<GroupPostCommentDto>.Success(_mapper.Map<GroupPostCommentDto>(result));
	}
}
