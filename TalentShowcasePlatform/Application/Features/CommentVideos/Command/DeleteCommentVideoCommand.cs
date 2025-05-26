using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CommentVideos.Command;

public class DeleteCommentVideoCommand : IRequest<Result<bool>>
{
	public Guid Id { get; set; }
}

public class DeleteCommentVideoCommandHandler : IRequestHandler<DeleteCommentVideoCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteCommentVideoCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<bool>> Handle(DeleteCommentVideoCommand request, CancellationToken cancellationToken)
	{
		var CommentVideo = await _unitOfWork.Repository<CommentVideo>().GetByIdAsync(request.Id);

		if (CommentVideo == null)
		{
			return Result<bool>.Failure("Không tìm thấy bình luận.");
		}

		await _unitOfWork.Repository<CommentVideo>().DeleteAsync(CommentVideo);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<bool>.Success(true);
		}
		else
		{
			return Result<bool>.Failure("Không thể xóa bình luận.");
		}
	}
}


