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

namespace Application.Features.Follows.Command;

public record DeleteFollowCommand : IRequest<Result<bool>> // Hoặc Result<Unit>
{
	public Guid FollowingUserId { get; set; }
	public Guid FollowedUserId { get; set; }
}

public class DeleteFollowCommandHandler : IRequestHandler<DeleteFollowCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteFollowCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<bool>> Handle(DeleteFollowCommand request, CancellationToken cancellationToken)
	{
		var item = await _unitOfWork.Repository<Follow>()
						.Entities
						.Where(F => F.FollowingUserId == request.FollowingUserId && F.FollowedUserId == request.FollowedUserId)
						.FirstOrDefaultAsync();
		if (item == null)
		{
			return Result<bool>.Failure("Người này đã không còn theo dõi.");
		}
		await _unitOfWork.Repository<Follow>().DeleteAsync(item);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<bool>.Success(true, "Hủy theo dõi thành công");
		}
		else
		{
			return Result<bool>.Failure("Hủy theo dõi không thành công.");
		}
	}
}
