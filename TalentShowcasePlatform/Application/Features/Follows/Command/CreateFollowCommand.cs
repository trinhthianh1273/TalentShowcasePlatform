using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Follows.Command;

public record CreateFollowCommand : IRequest<Result<bool>> // Hoặc Result<Unit> nếu bạn muốn trả về kết quả chi tiết hơn
{
	public Guid FollowingUserId { get; set; }
	public Guid FollowedUserId { get; set; }
}

public class CreateFollowCommandHandler : IRequestHandler<CreateFollowCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreateFollowCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<bool>> Handle(CreateFollowCommand request, CancellationToken cancellationToken)
	{
		var item = await _unitOfWork.Repository<Follow>()
						.Entities
						.Where(F => F.FollowingUserId == request.FollowingUserId && F.FollowedUserId == request.FollowedUserId)
						.FirstOrDefaultAsync();
		if (item != null)
		{
			return Result<bool>.Failure("Bạn đã theo dõi người này.");
		}
		await _unitOfWork.Repository<Follow>().AddAsync(item);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<bool>.Success(true, "Theo dõi thành công");
		}
		else
		{
			return Result<bool>.Failure("Theo dõi không thành công.");
		}
	}
}