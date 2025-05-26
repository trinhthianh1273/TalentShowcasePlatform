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

public class GetCommentVideoByIdQuery : IRequest<Result<CommentVideoDto>>
{
	public Guid Id { get; set; }
}

public class GetCommentVideoByIdQueryHandler : IRequestHandler<GetCommentVideoByIdQuery, Result<CommentVideoDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetCommentVideoByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<CommentVideoDto>> Handle(GetCommentVideoByIdQuery request, CancellationToken cancellationToken)
	{
		var CommentVideo = await _unitOfWork.Repository<CommentVideo>()
			.GetByIdAsync(request.Id, include: q => q.Include(c => c.Video).Include(c => c.User));

		if (CommentVideo == null)
		{
			return Result<CommentVideoDto>.Failure("Không tìm thấy bình luận.");
		}

		return Result<CommentVideoDto>.Success(_mapper.Map<CommentVideoDto>(CommentVideo));
	}
}


