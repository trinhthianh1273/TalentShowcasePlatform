using Application.Features.CommentVideos.Dto;
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

namespace Application.Features.CommentVideos.Query;

public class GetAllCommentVideosQuery : IRequest<Result<IEnumerable<CommentVideoDto>>>
{
}

public class GetAllCommentVideosQueryHandler : IRequestHandler<GetAllCommentVideosQuery, Result<IEnumerable<CommentVideoDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetAllCommentVideosQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<CommentVideoDto>>> Handle(GetAllCommentVideosQuery request, CancellationToken cancellationToken)
	{
		var CommentVideos = await _unitOfWork.Repository<CommentVideo>()
			.GetAllAsync(include: q => q.Include(c => c.Video).Include(c => c.User));
		return Result<IEnumerable<CommentVideoDto>>.Success(_mapper.Map<IEnumerable<CommentVideoDto>>(CommentVideos));
	}
}

