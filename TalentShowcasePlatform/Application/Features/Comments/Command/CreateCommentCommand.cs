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

public class CreateCommentCommand : IRequest<Result<Guid>>
{
	public Guid VideoId { get; set; }
	public Guid UserId { get; set; }
	public string Content { get; set; }
}

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Result<Guid>>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreateCommentCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<Guid>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
	{
		var comment = new Comment
		{
			Id = Guid.NewGuid(),
			VideoId = request.VideoId,
			UserId = request.UserId,
			Content = request.Content,
			CreatedAt = DateTime.UtcNow
		};

		await _unitOfWork.Repository<Comment>().AddAsync(comment);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<Guid>.Success(comment.Id);
		}
		else
		{
			return Result<Guid>.Failure("Không bình luận.");
		}
	}
}


