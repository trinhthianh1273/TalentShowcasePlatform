using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GroupPostComments.Command;

public record DeleteGroupPostCommentCommand : IRequest<Result<bool>>
{
	public Guid Id { get; set; }
}

public class DeleteGroupPostCommentCommandHandler : IRequestHandler<DeleteGroupPostCommentCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteGroupPostCommentCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<bool>> Handle(DeleteGroupPostCommentCommand request, CancellationToken cancellationToken)
	{
		var GroupPostComment = await _unitOfWork.Repository<CommentGroupPost>().GetByIdAsync(request.Id);

		if (GroupPostComment == null)
		{
			return Result<bool>.Failure("Không tìm thấy bài đăng.");
		}

		await _unitOfWork.Repository<CommentGroupPost>().DeleteAsync(GroupPostComment);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<bool>.Success(true, "Xóa thành công");
		}
		else
		{
			return Result<bool>.Failure("Không thể xóa comment.");
		}
	}
}
