using Application.Features.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Videos.Dto;

public class VideoDto
{
	public Guid Id { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public string Url { get; set; }
	public Guid UserId { get; set; }
	public Guid CategoryId { get; set; }
	public bool IsPrivate { get; set; }
	public DateTime UploadedAt { get; set; }
	public int LikeCount { get; set; } = 0;
	public int CommentCount { get; set; } = 0;
	public int ViewCount { get; set; } = 0;
	public UserNavigationDto UserNavigationDto { get; set; } // Navigation property to UserDto

}