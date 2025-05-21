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

public record DeleteRatingGroupPostCommand(Guid Id) : IRequest<Result<Guid>>;

public class DeleteRatingGroupPostCommandHandler : IRequestHandler<DeleteRatingGroupPostCommand, Result<Guid>>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteRatingGroupPostCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<Guid>> Handle(DeleteRatingGroupPostCommand request, CancellationToken cancellationToken)
	{
		var repo = _unitOfWork.Repository<RatingGroupPost>();
		var entity = await repo.GetByIdAsync(request.Id);

		if (entity == null)
			return Result<Guid>.Failure("Rating not found");

		await repo.DeleteAsync(entity);
		await _unitOfWork.Save(cancellationToken);

		return Result<Guid>.Success(request.Id);
	}
}

