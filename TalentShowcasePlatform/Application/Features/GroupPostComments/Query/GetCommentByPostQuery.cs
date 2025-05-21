using Application.Features.GroupPostComments.Dto;
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

public record GetCommentByPostQuery : IRequest<Result<IEnumerable<GroupPostCommentDto>>>
{
	public Guid PostId { get; set; }

	public GetCommentByPostQuery(Guid postId)
	{
		PostId = postId;
	}
}

public class GetCommentByPostQueryHandler : IRequestHandler<GetCommentByPostQuery, Result<IEnumerable<GroupPostCommentDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetCommentByPostQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<GroupPostCommentDto>>> Handle(GetCommentByPostQuery request, CancellationToken cancellationToken)
	{
		var GroupPostComments = await _unitOfWork.Repository<CommentGroupPost>().Entities
												.Where(c => c.GroupPostId == request.PostId)
												.Include(gp => gp.User) // Giả sử có navigation property User
												.Include(gp => gp.ChildComments) // Giả sử có navigation property ChildComments
												.ToListAsync(cancellationToken);

		var dtos = _mapper.Map<IEnumerable<GroupPostCommentDto>>(GroupPostComments);
		return Result<IEnumerable<GroupPostCommentDto>>.Success(dtos);
	}
}
