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

public record GetLikesByUserQuery(Guid UserId) : IRequest<Result<IEnumerable<LikeGroupPostDto>>>;

public class GetLikesByUserQueryHandler : IRequestHandler<GetLikesByUserQuery, Result<IEnumerable<LikeGroupPostDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetLikesByUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<LikeGroupPostDto>>> Handle(GetLikesByUserQuery request, CancellationToken cancellationToken)
	{
		var likes = await _unitOfWork.Repository<LikeGroupPost>().Entities
			.Where(l => l.UserId == request.UserId)
			.ToListAsync(cancellationToken);

		if (!likes.Any())
			return Result<IEnumerable<LikeGroupPostDto>>.Success(new List<LikeGroupPostDto>());

		var dtos = _mapper.Map<IEnumerable<LikeGroupPostDto>>(likes);
		return Result<IEnumerable<LikeGroupPostDto>>.Success(dtos);
	}
}
