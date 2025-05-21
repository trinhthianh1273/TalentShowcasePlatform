using Application.Features.GroupPostComments.Dto;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GroupPosts.Dto;	

public class GroupPostDto
{
	public Guid Id { get; set; }
	public string Title { get; set; }
	public string Content { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public DateTime? LastActivityDate { get; set; }
	public string ImgUrl { get; set; }
	public Guid UserId { get; set; }
	public string UserName { get; set; }
	public string UserImgUrl { get; set; }
	public Guid GroupId { get; set; }
	public string GroupName { get; set; }
	public int? LikeCount { get; set; }
	public int? CommentCount { get; set; }

	public ICollection<GroupPostCommentDto> GroupPostComments { get; set; }
}
