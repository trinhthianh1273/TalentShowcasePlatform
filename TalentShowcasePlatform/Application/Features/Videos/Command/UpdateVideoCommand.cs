using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Videos.Command;

public class UpdateVideoCommand : IRequest<Result<bool>>
{
	public Guid Id { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public string Url { get; set; }
	public Guid CategoryId { get; set; }
	public bool IsPrivate { get; set; }
}

public class UpdateVideoHandler : IRequestHandler<UpdateVideoCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public UpdateVideoHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<bool>> Handle(UpdateVideoCommand request, CancellationToken cancellationToken)
	{
		var video = await _unitOfWork.Repository<Video>().GetByIdAsync(request.Id);

		if (video == null)
		{
			return Result<bool>.Failure("Video not found.");
		}

		video.Title = request.Title;
		video.Description = request.Description;
		video.Url = request.Url;
		video.CategoryId = request.CategoryId;
		video.IsPrivate = request.IsPrivate;

		await _unitOfWork.Repository<Video>().UpdateAsync(video);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<bool>.Success(true);
		}
		else
		{
			return Result<bool>.Failure("Failed to update video.");
		}
	}
}



