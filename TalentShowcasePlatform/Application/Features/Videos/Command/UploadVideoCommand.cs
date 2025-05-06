using Application.Features.Videos.Dto;
using Application.Services;
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

public record UploadVideoCommand : IRequest<Result<VideoDto>>
{
	public string Title { get; set; }
	public string Description { get; set; }
	public IFormFile VideoFile { get; set; }
	public Guid CategoryId { get; set; }
	public bool IsPrivate { get; set; }
	public Guid UserId { get; set; } // Lấy từ thông tin người dùng đã xác thực
}

public class UploadVideoHandler : IRequestHandler<UploadVideoCommand, Result<VideoDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IFileService _fileService;
	private readonly IMapper _mapper;

	public UploadVideoHandler(IUnitOfWork unitOfWork, IFileService fileService, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_fileService = fileService;
		_mapper = mapper;
	}

	public async Task<Result<VideoDto>> Handle(UploadVideoCommand request, CancellationToken cancellationToken)
	{
		// 1. Xử lý file video
		var fileResult = await _fileService.SaveFileAsync(request.VideoFile, "Assest/Video"); // Lưu vào thư mục "videos"
		if (!fileResult.Succeeded)
		{
			return Result<VideoDto>.Failure(fileResult.Messages);
		}

		// 2. Tạo Video entity
		var video = new Video
		{
			Id = Guid.NewGuid(),
			Title = request.Title,
			Description = request.Description,
			Url = fileResult.Data, // Đường dẫn đến file đã lưu
			UserId = request.UserId,
			CategoryId = request.CategoryId,
			IsPrivate = request.IsPrivate,
			UploadedAt = DateTime.UtcNow
		};

		// 3. Lưu vào database
		await _unitOfWork.Repository<Video>().AddAsync(video);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<VideoDto>.Success(_mapper.Map<VideoDto>(video), "Video uploaded successfully.");
		}
		else
		{
			// Xóa file nếu lưu DB thất bại (quan trọng!)
			await _fileService.DeleteFileAsync(fileResult.Data, "videos");
			return Result<VideoDto>.Failure("Failed to save video data.");
		}
	}
}
