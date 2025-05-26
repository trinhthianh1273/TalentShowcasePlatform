using Application.Features.GroupPostComments.Dto;
using Application.Features.GroupPosts.Dto;
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

namespace Application.Features.GroupPosts.Query;

public record GetAllCommentByGroupQuery : IRequest<Result<IEnumerable<GroupPostDto>>>
{
}

public class GetAllGroupPostsQueryHandler : IRequestHandler<GetAllCommentByGroupQuery, Result<IEnumerable<GroupPostDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetAllGroupPostsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<GroupPostDto>>> Handle(GetAllCommentByGroupQuery request, CancellationToken cancellationToken)
	{
		var groupPosts = await _unitOfWork.Repository<GroupPost>()
			.GetAllAsync(include: q => q
				.Include(gp => gp.User) // Giả sử có navigation property User
				.Include(gp => gp.Group));
		var postDtos = new List<GroupPostDto>();
		foreach (var post in groupPosts)
		{
			var postDto = _mapper.Map<GroupPostDto>(post);
			var comments = await _unitOfWork.Repository<CommentGroupPost>().Entities
											.Where(gc => gc.GroupPostId == post.Id)
											.ToListAsync(cancellationToken);
			postDto.GroupPostComments = _mapper.Map<List<GroupPostCommentDto>>(comments);
			postDtos.Add(postDto);
		}

		//var dtos = _mapper.Map<IEnumerable<GroupPostDto>>(postDto);
		return Result<IEnumerable<GroupPostDto>>.Success(postDtos);
	}
}
