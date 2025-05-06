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

public class CreateVideoCommand : IRequest<Result<Guid>>
{
	public string Title { get; set; }
	public string Description { get; set; }
	public string Url { get; set; }
	public Guid UserId { get; set; }
	public Guid CategoryId { get; set; }
	public bool IsPrivate { get; set; }
	public DateTime UploadedAt { get; set; }
}

public class CreateVideoHandler : IRequestHandler<CreateVideoCommand, Result<Guid>>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreateVideoHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<Guid>> Handle(CreateVideoCommand request, CancellationToken cancellationToken)
	{
		var video = new Video
		{
			Id = Guid.NewGuid(),
			Title = request.Title,
			Description = request.Description,
			Url = request.Url,
			UserId = request.UserId,
			CategoryId = request.CategoryId,
			IsPrivate = request.IsPrivate,
			UploadedAt = request.UploadedAt
		};

		await _unitOfWork.Repository<Video>().AddAsync(video);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<Guid>.Success(video.Id);
		}
		else
		{
			return Result<Guid>.Failure("Failed to create video.");
		}
	}
}



