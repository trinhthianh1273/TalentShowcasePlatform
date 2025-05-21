using Application.Features.GroupPosts.Dto;
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

namespace Application.Features.GroupPosts.Query;

public record GetCommentByIdQuery : IRequest<Result<GroupPostDto>>
{
	public Guid Id { get; set; }
}

internal class GetAllGroupPostsByIdQueryHandler : IRequestHandler<GetCommentByIdQuery, Result<GroupPostDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetAllGroupPostsByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<GroupPostDto>> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
	{
		var result = await _unitOfWork.Repository<GroupPost>()
										.GetByIdAsync(request.Id, include: q => q.Include(g => g.User).Include(g => g.Group));

		if (result == null)
		{
			return Result<GroupPostDto>.Failure("Not Found.");
		}

		return Result<GroupPostDto>.Success(_mapper.Map<GroupPostDto>(result));
	}
}
