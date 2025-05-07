using Application.Features.Comments.Dto;
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

namespace Application.Features.Comments.Query;

public class GetCommentsByVideoIdQuery : IRequest<Result<IEnumerable<CommentDto>>>
{
	public Guid VideoId { get; set; }
}

public class GetCommentsByVideoIdQueryHandler : IRequestHandler<GetCommentsByVideoIdQuery, Result<IEnumerable<CommentDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetCommentsByVideoIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<CommentDto>>> Handle(GetCommentsByVideoIdQuery request, CancellationToken cancellationToken)
	{
		var comments = _unitOfWork.Repository<Comment>().Entities
							.Where(c => c.VideoId == request.VideoId)
							.OrderByDescending(c => c.CreatedAt)
							.Include(c => c.Video).Include(c => c.User);
		return Result<IEnumerable<CommentDto>>.Success(_mapper.Map<IEnumerable<CommentDto>>(comments));
	}
}
