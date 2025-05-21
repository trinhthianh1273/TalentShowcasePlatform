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

public record GetAllCommentPostQuery : IRequest<Result<IEnumerable<GroupPostCommentDto>>>
{
}

public class GetAllCommentPostQueryHandler : IRequestHandler<GetAllCommentPostQuery, Result<IEnumerable<GroupPostCommentDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetAllCommentPostQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<GroupPostCommentDto>>> Handle(GetAllCommentPostQuery request, CancellationToken cancellationToken)
	{
		var GroupPostComments = await _unitOfWork.Repository<CommentGroupPost>()
			.GetAllAsync(include: q => q
				.Include(gp => gp.User) // Giả sử có navigation property User
				.Include(gp => gp.ChildComments)); // Giả sử có navigation property Group

		var dtos = _mapper.Map<IEnumerable<GroupPostCommentDto>>(GroupPostComments);
		return Result<IEnumerable<GroupPostCommentDto>>.Success(dtos);
	}
}
