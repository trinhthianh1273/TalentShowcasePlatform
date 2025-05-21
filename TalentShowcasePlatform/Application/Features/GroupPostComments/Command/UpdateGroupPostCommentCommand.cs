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

public record UpdateGroupPostCommentCommand : IRequest<Result<bool>>
{
	public Guid Id { get; set; }
	public string Content { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class UpdateGroupPostCommentCommandHandler : IRequestHandler<UpdateGroupPostCommentCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public UpdateGroupPostCommentCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<bool>> Handle(UpdateGroupPostCommentCommand request, CancellationToken cancellationToken)
	{
		var existingGroupPostComment = await _unitOfWork.Repository<CommentGroupPost>().GetByIdAsync(request.Id);

		if (existingGroupPostComment == null)
		{
			return Result<bool>.Failure("Lỗi khi sửa.");
		}

		existingGroupPostComment.Content = request.Content;
		existingGroupPostComment.CreatedAt = request.CreatedAt;

		await _unitOfWork.Repository<CommentGroupPost>().UpdateAsync(existingGroupPostComment);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<bool>.Success(true, "Thành công");
		}
		else
		{
			return Result<bool>.Failure("Không thể sửa comment.");
		}
	}
}