using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Videos.Dto;

public class UploadVideoRequestDto
{
	public string Title { get; set; }
	public string Description { get; set; }
	public IFormFile VideoFile { get; set; } // Để nhận file từ request
	public Guid CategoryId { get; set; }
	public bool IsPrivate { get; set; }
}
