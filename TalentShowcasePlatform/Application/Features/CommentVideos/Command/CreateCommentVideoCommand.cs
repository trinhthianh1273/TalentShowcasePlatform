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

public class CreateCommentVideoCommand : IRequest<Result<Guid>>
{
	public Guid VideoId { get; set; }
	public Guid UserId { get; set; }
	public string Content { get; set; }
}

public class CreateCommentVideoCommandHandler : IRequestHandler<CreateCommentVideoCommand, Result<Guid>>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreateCommentVideoCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<Guid>> Handle(CreateCommentVideoCommand request, CancellationToken cancellationToken)
	{
		var CommentVideo = new CommentVideo
		{
			Id = Guid.NewGuid(),
			VideoId = request.VideoId,
			UserId = request.UserId,
			Content = request.Content,
			CreatedAt = DateTime.UtcNow
		};

		await _unitOfWork.Repository<CommentVideo>().AddAsync(CommentVideo);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<Guid>.Success(CommentVideo.Id);
		}
		else
		{
			return Result<Guid>.Failure("Không bình luận.");
		}
	}
}


