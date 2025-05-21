using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GroupPosts.Command;

public record DeleteGroupPostCommand : IRequest<Result<bool>>
{
	public Guid Id { get; set; }
}

public class DeleteGroupPostCommandHandler : IRequestHandler<DeleteGroupPostCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteGroupPostCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<bool>> Handle(DeleteGroupPostCommand request, CancellationToken cancellationToken)
	{
		var groupPost = await _unitOfWork.Repository<GroupPost>().GetByIdAsync(request.Id);

		if (groupPost == null)
		{
			return Result<bool>.Failure("Không tìm thấy bài đăng.");
		}

		await _unitOfWork.Repository<GroupPost>().DeleteAsync(groupPost);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<bool>.Success(true, "Xóa thành công");
		}
		else
		{
			return Result<bool>.Failure("Không thể xóa bài đăng.");
		}
	}
}
