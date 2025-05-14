using Application.Features.Users.Dto;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shared.Interfaces;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Command;

public record UploadAvatarCommand : IRequest<Result<UserDto>>
{
	public Guid UserId { get; set; }
	public IFormFile File { get; set; }
}

public class UploadAvatarHandler : IRequestHandler<UploadAvatarCommand, Result<UserDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public UploadAvatarHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<UserDto>> Handle(UploadAvatarCommand request, CancellationToken cancellationToken)
	{
		var user = await _unitOfWork.Repository<User>().GetByIdAsync(request.UserId);
		if (user == null)
			return Result<UserDto>.Failure("User not found");


		var rootPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."); // Lùi lên 3 cấp từ bin/Debug/netX.X/
		var avatarFolder = Path.Combine(rootPath, "Assets", "Avatars");


		// Lấy thư mục gốc của dự án bằng cách di chuyển lên từ thư mục hiện tại
		var projectRootPath = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
		// Kết hợp đường dẫn gốc với thư mục Assets/Videos
		var imagePath = Path.Combine(projectRootPath, "Assets", "Avatars");


		// Xóa ảnh cũ nếu có
		if (!string.IsNullOrEmpty(user.AvatarUrl))
		{
			var oldAvatarPath = Path.Combine(avatarFolder, user.AvatarUrl);
			if (File.Exists(oldAvatarPath))
				File.Delete(oldAvatarPath);
		}
		// Lưu ảnh mới
		var fileName = $"{request.UserId}{Path.GetExtension(request.File.FileName)}";
		var newAvatarPath = Path.Combine(avatarFolder, fileName);

		using (var stream = new FileStream(newAvatarPath, FileMode.Create))
		{
			await request.File.CopyToAsync(stream);
		}

		user.AvatarUrl = fileName;
		await _unitOfWork.Repository<User>().UpdateAsync(user);
		var saveResult = await _unitOfWork.Save(cancellationToken);
		if (saveResult > 0)
		{
			return Result<UserDto>.Success(_mapper.Map<UserDto>(user));
		}
		else
		{
			return Result<UserDto>.Failure("Failed to change avatar.");
		}
	}
}