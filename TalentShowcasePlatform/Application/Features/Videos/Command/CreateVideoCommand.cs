using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Videos.Command;

public class CreateVideoCommand : IRequest<Result<Guid>>
{
	public IFormFile? File { get; set; }
	public string Title { get; set; }
	public string? Description { get; set; }
	public string? Url { get; set; }
	public Guid UserId { get; set; }
	public Guid? CategoryId { get; set; }
	public bool IsPrivate { get; set; }
}

public class CreateVideoHandler : IRequestHandler<CreateVideoCommand, Result<Guid>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public CreateVideoHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<Guid>> Handle(CreateVideoCommand request, CancellationToken cancellationToken)
	{
		var rootPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..");
		var videoFolder = Path.Combine(rootPath, "Assets", "Videos");

		var fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.File.FileName)}";
		var videoPath = Path.Combine(videoFolder, fileName);

		using (var stream = new FileStream(videoPath, FileMode.Create))
		{
			await request.File.CopyToAsync(stream);
		}

		var video = new Video
		{
			Id = Guid.NewGuid(),
			Title = request.Title,
			Description = request.Description,
			Url = request.File != null ? fileName : request.Url,
			UserId = request.UserId,
			CategoryId = request.CategoryId ,
			IsPrivate = request.IsPrivate,
			UploadedAt = DateTime.UtcNow
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



