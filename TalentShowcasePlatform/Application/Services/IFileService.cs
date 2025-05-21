using Microsoft.AspNetCore.Http;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services;

public interface IFileService
{
	Task<Result<string>> SaveFileAsync(IFormFile file, string folderName); // Trả về đường dẫn
	Task<Result<bool>> DeleteFileAsync(string filePath, string folderName);
	Task<Result<string>> UploadFileAsync(Guid id, IFormFile file, string folderName);
}
