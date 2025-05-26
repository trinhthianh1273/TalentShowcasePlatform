using Application.Features.Users.Dto;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Videos.Dto;

class VideoDetailDto
{
	public Guid Id { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public string Url { get; set; }
	public Guid UserId { get; set; }
	public Guid CategoryId { get; set; }
	public bool IsPrivate { get; set; }
	public DateTime UploadedAt { get; set; }
	public UserNavigationDto UserNavigationDto { get; set; } // Navigation property to UserDto
	public List<CommentVideo> CommentVideos { get; set; } = new List<CommentVideo>();
	public List<VideoLike> VideoLikes { get; set; } = new List<VideoLike>();
}
