using Application.Features.CommentVideos.Dto;
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

namespace Application.Features.CommentVideos.Command;

public class CreateCommentVideoListCommand : IRequest<Result<List<CommentVideoDto>>>
{
	public List<CreateCommentVideoCommand> CommentVideos { get; set; }
}

public class CreateCommentVideoListCommandHandler : IRequestHandler<CreateCommentVideoListCommand, Result<List<CommentVideoDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public CreateCommentVideoListCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<List<CommentVideoDto>>> Handle(CreateCommentVideoListCommand request, CancellationToken cancellationToken)
	{
		var CommentVideos = new List<CommentVideo>();
		foreach (var videoDto in request.CommentVideos)
		{
			var CommentVideo = new CommentVideo
			{
				Id = Guid.NewGuid(),
				UserId = videoDto.UserId,
				VideoId = videoDto.VideoId,
				Content = videoDto.Content
			};
			CommentVideos.Add(CommentVideo);
		}

		await _unitOfWork.Repository<CommentVideo>().AddRangeAsync(CommentVideos);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			var createdCommentVideoDtos = _mapper.Map<List<CommentVideoDto>>(CommentVideos);
			return Result<List<CommentVideoDto>>.Success(createdCommentVideoDtos, "CommentVideoed successfully.");
		}
		else
		{
			return Result<List<CommentVideoDto>>.Failure("Không bình luận.");
		}
	}
}


