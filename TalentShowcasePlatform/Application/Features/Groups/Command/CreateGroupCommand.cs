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
using System.Threading.Tasks;

namespace Application.Features.Groups.Command;


public record CreateGroupCommand : IRequest<Result<Guid>>
{
	public string Name { get; set; }
	public string Description { get; set; }
	public Guid CategoryId { get; set; }
	public Guid CreatedBy { get; set; }
	public IFormFile? GroupAvatar { get; set; } // Upload file
}

public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, Result<Guid>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IFileService _fileService;

	public CreateGroupCommandHandler(IUnitOfWork unitOfWork, IFileService fileService)
	{
		_unitOfWork = unitOfWork;
		_fileService = fileService;
	}

	public async Task<Result<Guid>> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
	{
		Console.WriteLine($"Name: {request.Name}");
		Console.WriteLine($"CategoryId: {request.CategoryId}");
		Console.WriteLine($"CreatedBy: {request.CreatedBy}");

		try
		{
			var groupId = Guid.NewGuid();

			string? avatarFileName = $"{groupId}{Path.GetExtension(request.GroupAvatar.FileName)}";
			if (request.GroupAvatar != null)
			{
				var uploadResult = await _fileService.UploadFileAsync(groupId, request.GroupAvatar, "GroupAvatar"); //Guid id, IFormFile file, string folderName
				if (!uploadResult.Succeeded)
					return Result<Guid>.Failure(uploadResult.Messages); // 🛑 Nếu lỗi -> dừng lại

				 //avatarFileName = uploadResult.Data;
				Console.WriteLine($"AvatarFileName: {avatarFileName}");
			}

			var group = new Group
			{
				Id = groupId,
				Name = request.Name ?? string.Empty,
				Description = request.Description ?? string.Empty,
				CategoryId = request.CategoryId,
				CreatedBy = request.CreatedBy,
				CreatedAt = DateTime.UtcNow,
				GroupAvatar = avatarFileName // 👈 tránh null
			};

			await _unitOfWork.Repository<Group>().AddAsync(group);
			try
			{
				var saveResult = await _unitOfWork.Save(cancellationToken);

				if (saveResult > 0)
					return Result<Guid>.Success(group.Id);

				return Result<Guid>.Failure("Không thể tạo nhóm.");
			}
			catch (Exception ex)
			{
				return Result<Guid>.Failure($"Lỗi khi lưu nhóm: {ex.Message} - {ex.InnerException?.Message}");
			}
			return Result<Guid>.Success(groupId);
			//var saveResult = await _unitOfWork.Save(cancellationToken);
		}
		catch (Exception ex)
		{
			return Result<Guid>.Failure($"Lỗi: {ex.Message}"); // 👈 tránh ASP.NET tự ném ra HTML error
		}
	}
}
