using Application.Features.RatingGroupPosts.Dto;
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

namespace Application.Features.RatingGroupPosts.Command;

public record CreateRatingGroupPostCommand(Guid GroupPostId, Guid UserId, int Value) : IRequest<Result<RatingGroupPostDto>>;

public class CreateRatingGroupPostCommandHandler : IRequestHandler<CreateRatingGroupPostCommand, Result<RatingGroupPostDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public CreateRatingGroupPostCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<RatingGroupPostDto>> Handle(CreateRatingGroupPostCommand request, CancellationToken cancellationToken)
	{
		var entity = new RatingGroupPost
		{
			Id = Guid.NewGuid(),
			GroupPostId = request.GroupPostId,
			UserId = request.UserId,
			Value = request.Value,
			CreatedAt = DateTime.UtcNow
		};

		await _unitOfWork.Repository<RatingGroupPost>().AddAsync(entity);
		await _unitOfWork.Save(cancellationToken);

		var dto = _mapper.Map<RatingGroupPostDto>(entity);
		return Result<RatingGroupPostDto>.Success(dto);
	}
}

