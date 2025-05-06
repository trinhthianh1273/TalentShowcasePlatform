using Application.Features.Comments.Dto;
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

namespace Application.Features.Comments.Query;

public class GetCommentByIdQuery : IRequest<Result<CommentDto>>
{
	public Guid Id { get; set; }
}

public class GetCommentByIdQueryHandler : IRequestHandler<GetCommentByIdQuery, Result<CommentDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetCommentByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<CommentDto>> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
	{
		var comment = await _unitOfWork.Repository<Comment>()
			.GetByIdAsync(request.Id, include: q => q.Include(c => c.Video).Include(c => c.User));

		if (comment == null)
		{
			return Result<CommentDto>.Failure("Không tìm thấy bình luận.");
		}

		return Result<CommentDto>.Success(_mapper.Map<CommentDto>(comment));
	}
}


