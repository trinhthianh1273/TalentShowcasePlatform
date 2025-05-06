using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services;

public class FileService : IFileService
{
	private readonly IHostEnvironment _environment;

	public FileService(IHostEnvironment environment)
	{
		_environment = environment;
	}

	public async Task<Result<string>> SaveFileAsync(IFormFile file, string folderName)
	{
		if (file == null || file.Length == 0)
		{
			return Result<string>.Failure("Invalid file.");
		}

		string contentRootPath = _environment.ContentRootPath;
		string webRootPath = Path.Combine(contentRootPath, "wwwroot"); // Đường dẫn đến wwwroot
		string folderPath = Path.Combine(webRootPath, folderName);

		if (!Directory.Exists(folderPath))
		{
			Directory.CreateDirectory(folderPath);
		}

		string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
		string filePath = Path.Combine(folderPath, fileName);

		try
		{
			using (var fileStream = new FileStream(filePath, FileMode.Create))
			{
				await file.CopyToAsync(fileStream);
			}

			return Result<string>.Success("/" + folderName + "/" + fileName, "File saved successfully.");
		}
		catch (Exception ex)
		{
			return Result<string>.Failure("Failed to save file.");
		}
	}

	public async Task<Result<bool>> DeleteFileAsync(string filePath, string folderName)
	{
		string contentRootPath = _environment.ContentRootPath;
		string fullPath = Path.Combine(contentRootPath, "wwwroot", folderName, Path.GetFileName(filePath));

		if (File.Exists(fullPath))
		{
			try
			{
				File.Delete(fullPath);
				return Result<bool>.Success(true, "File deleted successfully.");
			}
			catch (Exception ex)
			{
				return Result<bool>.Failure("Failed to delete file.");
			}
		}

		return Result<bool>.Failure("File not found.");
	}
}

