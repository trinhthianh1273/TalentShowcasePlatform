using Application.Features.LikeGroupPosts.Dto;
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

namespace Application.Features.LikeGroupPosts.Query;

public record GetLikeGroupPostByIdQuery(Guid Id) : IRequest<Result<LikeGroupPostDto>>;

public class GetLikeGroupPostByIdQueryHandler : IRequestHandler<GetLikeGroupPostByIdQuery, Result<LikeGroupPostDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetLikeGroupPostByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<LikeGroupPostDto>> Handle(GetLikeGroupPostByIdQuery request, CancellationToken cancellationToken)
	{
		var like = await _unitOfWork.Repository<LikeGroupPost>().Entities
									.Where(i => i.GroupPostId == request.Id)
									.Include(i => i.User)
									.FirstOrDefaultAsync(cancellationToken);

		if (like is null)
			return Result<LikeGroupPostDto>.Failure("Like not found.");

		var dto = _mapper.Map<LikeGroupPostDto>(like);
		return Result<LikeGroupPostDto>.Success(dto);
	}
}

