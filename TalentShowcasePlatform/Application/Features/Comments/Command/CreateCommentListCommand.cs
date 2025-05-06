using Application.Features.Comments.Dto;
using Application.Features.Videos.Command;
using Application.Features.Videos.Dto;
using AutoMapper;
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

public class CreateCommentListCommand : IRequest<Result<List<CommentDto>>>
{
	public List<CreateCommentCommand> Comments { get; set; }
}

public class CreateCommentListCommandHandler : IRequestHandler<CreateCommentListCommand, Result<List<CommentDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public CreateCommentListCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<List<CommentDto>>> Handle(CreateCommentListCommand request, CancellationToken cancellationToken)
	{
		var comments = new List<Comment>();
		foreach (var videoDto in request.Comments)
		{
			var comment = new Comment
			{
				Id = Guid.NewGuid(),
				UserId = videoDto.UserId,
				VideoId = videoDto.VideoId,
				Content = videoDto.Content
			};
			comments.Add(comment);
		}

		await _unitOfWork.Repository<Comment>().AddRangeAsync(comments);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			var createdCommentDtos = _mapper.Map<List<CommentDto>>(comments);
			return Result<List<CommentDto>>.Success(createdCommentDtos, "Commented successfully.");
		}
		else
		{
			return Result<List<CommentDto>>.Failure("Không bình luận.");
		}
	}
}


