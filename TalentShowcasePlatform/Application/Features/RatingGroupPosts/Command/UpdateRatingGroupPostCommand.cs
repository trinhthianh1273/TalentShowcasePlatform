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

public record UpdateRatingGroupPostCommand(Guid Id, int Value) : IRequest<Result<RatingGroupPostDto>>;

public class UpdateRatingGroupPostCommandHandler : IRequestHandler<UpdateRatingGroupPostCommand, Result<RatingGroupPostDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public UpdateRatingGroupPostCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<RatingGroupPostDto>> Handle(UpdateRatingGroupPostCommand request, CancellationToken cancellationToken)
	{
		var repo = _unitOfWork.Repository<RatingGroupPost>();
		var entity = await repo.GetByIdAsync(request.Id);

		if (entity == null)
			return Result<RatingGroupPostDto>.Failure("Rating not found");

		entity.Value = request.Value;
		entity.CreatedAt = DateTime.UtcNow;

		await repo.UpdateAsync(entity);
		await _unitOfWork.Save(cancellationToken);

		var dto = _mapper.Map<RatingGroupPostDto>(entity);
		return Result<RatingGroupPostDto>.Success(dto);
	}
}

