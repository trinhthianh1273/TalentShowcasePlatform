using Application.Features.RatingGroupPosts.Dto;
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

namespace Application.Features.RatingGroupPosts.Query;

public record GetRatingGroupPostByIdQuery(Guid Id) : IRequest<Result<RatingGroupPostDto>>;

public class GetRatingGroupPostByIdQueryHandler : IRequestHandler<GetRatingGroupPostByIdQuery, Result<RatingGroupPostDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetRatingGroupPostByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<RatingGroupPostDto>> Handle(GetRatingGroupPostByIdQuery request, CancellationToken cancellationToken)
	{
		var entity = await _unitOfWork.Repository<RatingGroupPost>().Entities
										.Where(i => i.GroupPostId == request.Id)
										.Include(i => i.User)
										.FirstOrDefaultAsync(cancellationToken); ;
		if (entity == null)
			return Result<RatingGroupPostDto>.Failure("Rating not found");

		var dto = _mapper.Map<RatingGroupPostDto>(entity);
		return Result<RatingGroupPostDto>.Success(dto);
	}
}
