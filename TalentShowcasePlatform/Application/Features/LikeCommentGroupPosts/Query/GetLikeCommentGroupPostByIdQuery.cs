using Application.Features.LikeCommentGroupPosts.Dto;
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

namespace Application.Features.LikeCommentGroupPosts.Query;

public record GetLikeCommentGroupPostByIdQuery(Guid Id) : IRequest<Result<LikeCommentGroupPostDto>>;
public class GetLikeCommentGroupPostByIdQueryHandler : IRequestHandler<GetLikeCommentGroupPostByIdQuery, Result<LikeCommentGroupPostDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetLikeCommentGroupPostByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<LikeCommentGroupPostDto>> Handle(GetLikeCommentGroupPostByIdQuery request, CancellationToken cancellationToken)
	{
		var like = await _unitOfWork.Repository<LikeCommentGroupPost>().GetByIdAsync(request.Id);

		if (like == null)
			return Result<LikeCommentGroupPostDto>.Failure("Like not found.");

		var dto = _mapper.Map<LikeCommentGroupPostDto>(like);
		return Result<LikeCommentGroupPostDto>.Success(dto);
	}
}

