using Application.Features.CommentVideos.Dto;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.CommentVideos.Query;

public class GetCommentVideosByVideoIdQuery : IRequest<Result<IEnumerable<CommentVideoDto>>>
{
	public Guid VideoId { get; set; }
}

public class GetCommentVideosByVideoIdQueryHandler : IRequestHandler<GetCommentVideosByVideoIdQuery, Result<IEnumerable<CommentVideoDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetCommentVideosByVideoIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<CommentVideoDto>>> Handle(GetCommentVideosByVideoIdQuery request, CancellationToken cancellationToken)
	{
		var CommentVideos = _unitOfWork.Repository<CommentVideo>().Entities
							.Where(c => c.VideoId == request.VideoId)
							.OrderByDescending(c => c.CreatedAt)
							.Include(c => c.Video).Include(c => c.User);
		return Result<IEnumerable<CommentVideoDto>>.Success(_mapper.Map<IEnumerable<CommentVideoDto>>(CommentVideos));
	}
}
