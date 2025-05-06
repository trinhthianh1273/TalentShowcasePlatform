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

public class GetAllCommentsQuery : IRequest<Result<IEnumerable<CommentDto>>>
{
}

public class GetAllCommentsQueryHandler : IRequestHandler<GetAllCommentsQuery, Result<IEnumerable<CommentDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetAllCommentsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<CommentDto>>> Handle(GetAllCommentsQuery request, CancellationToken cancellationToken)
	{
		var comments = await _unitOfWork.Repository<Comment>()
			.GetAllAsync(include: q => q.Include(c => c.Video).Include(c => c.User));
		return Result<IEnumerable<CommentDto>>.Success(_mapper.Map<IEnumerable<CommentDto>>(comments));
	}
}

