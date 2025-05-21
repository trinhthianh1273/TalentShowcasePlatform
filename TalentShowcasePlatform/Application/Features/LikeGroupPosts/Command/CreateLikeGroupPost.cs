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

namespace Application.Features.LikeGroupPosts.Command;

public record CreateLikeGroupPostCommand(Guid UserId, Guid GroupPostId) : IRequest<Result<LikeGroupPostDto>>;

public class CreateLikeGroupPostCommandHandler : IRequestHandler<CreateLikeGroupPostCommand, Result<LikeGroupPostDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public CreateLikeGroupPostCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<LikeGroupPostDto>> Handle(CreateLikeGroupPostCommand request, CancellationToken cancellationToken)
	{
		var exists = await _unitOfWork.Repository<LikeGroupPost>().Entities
			.AnyAsync(x => x.UserId == request.UserId && x.GroupPostId == request.GroupPostId, cancellationToken);

		if (exists)
			return Result<LikeGroupPostDto>.Failure("User already liked this post.");

		var like = new LikeGroupPost
		{
			Id = Guid.NewGuid(),
			UserId = request.UserId,
			GroupPostId = request.GroupPostId,
			CreatedAt = DateTime.UtcNow
		};

		await _unitOfWork.Repository<LikeGroupPost>().AddAsync(like);
		await _unitOfWork.Save(cancellationToken);

		var dto = _mapper.Map<LikeGroupPostDto>(like);
		return Result<LikeGroupPostDto>.Success(dto);
	}
}
