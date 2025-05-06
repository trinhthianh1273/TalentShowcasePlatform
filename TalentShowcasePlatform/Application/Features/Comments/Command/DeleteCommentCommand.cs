using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comments.Command;

public class DeleteCommentCommand : IRequest<Result<bool>>
{
	public Guid Id { get; set; }
}

public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteCommentCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<bool>> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
	{
		var comment = await _unitOfWork.Repository<Comment>().GetByIdAsync(request.Id);

		if (comment == null)
		{
			return Result<bool>.Failure("Không tìm thấy bình luận.");
		}

		await _unitOfWork.Repository<Comment>().DeleteAsync(comment);
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


