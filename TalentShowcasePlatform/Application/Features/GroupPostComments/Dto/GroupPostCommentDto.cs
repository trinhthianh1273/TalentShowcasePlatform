using Application.Features.Users.Dto;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GroupPostComments.Dto;	

public class GroupPostCommentDto
{
	public Guid GroupPostId { get; set; }
	public Guid UserId { get; set; }
	public string Content { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public Guid? ParentCommentId { get; set; }
	public string? UserName { get; set; }
	public string? UserImgUrl { get; set; }
	//public ICollection<GroupPostCommentDto> ChildComments { get; set; } // Navigation property for child comments
}
