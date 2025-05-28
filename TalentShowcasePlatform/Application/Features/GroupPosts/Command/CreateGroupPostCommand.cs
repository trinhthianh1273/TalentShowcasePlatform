using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Features.GroupPosts.Command;

public record CreateGroupPostCommand : IRequest<Result<Guid>>
{
	public string Title { get; set; }
	public string Content { get; set; }
	public IFormFile? ImgFile { get; set; }
	public Guid UserId { get; set; }
	public Guid GroupId { get; set; }
}

public class CreateGroupPostCommandHandler : IRequestHandler<CreateGroupPostCommand, Result<Guid>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IFileService _fileService;
	private readonly IActivityEventPublisher _activityEventPublisher;

	public CreateGroupPostCommandHandler(IUnitOfWork unitOfWork, IFileService fileService, IActivityEventPublisher activityEventPublisher)
	{
		_unitOfWork = unitOfWork;
		_fileService = fileService;
		_activityEventPublisher = activityEventPublisher;
	}

	public async Task<Result<Guid>> Handle(CreateGroupPostCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var groupPostId = Guid.NewGuid();
			string? groupPostImgUrl = $"{groupPostId}{Path.GetExtension(request.ImgFile.FileName)}";
			if (request.ImgFile != null)
			{
				var uploadResult = await _fileService.UploadFileAsync(groupPostId, request.ImgFile, "GroupPosts"); //Guid id, IFormFile file, string folderName
				if (!uploadResult.Succeeded)
					return Result<Guid>.Failure(uploadResult.Messages); // 🛑 Nếu lỗi -> dừng lại

				//avatarFileName = uploadResult.Data;
				Console.WriteLine($"AvatarFileName: {groupPostImgUrl}");
			}
			var newGroupPost = new GroupPost
			{
				Id = groupPostId,
				Title = request.Title,
				Content = request.Content,
				UserId = request.UserId,
				GroupId = request.GroupId,
				ImgUrl = groupPostImgUrl // 👈 tránh nul
			};

			await _unitOfWork.Repository<GroupPost>().AddAsync(newGroupPost);
			var saveResult = await _unitOfWork.Save(cancellationToken);

			if (saveResult > 0)
			{
				var group = await _unitOfWork.Repository<Domain.Entities.Group>().GetByIdAsync(request.GroupId);
				// Tạo notification
				await _activityEventPublisher.PublishCreatePostInGroupAsync(newGroupPost.UserId, group.CreatedBy, newGroupPost.GroupId, newGroupPost.Id);

				return Result<Guid>.Success(newGroupPost.Id);
			}
			else
			{
				return Result<Guid>.Failure("Không thể tạo bài đăng.");
			}
		}
		catch (Exception ex)
		{
			return Result<Guid>.Failure($"Lỗi: {ex.Message}"); // 👈 tránh ASP.NET tự ném ra HTML error
		}
		
	}
}
