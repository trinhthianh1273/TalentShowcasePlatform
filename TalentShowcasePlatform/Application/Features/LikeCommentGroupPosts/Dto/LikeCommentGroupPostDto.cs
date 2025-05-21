using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LikeCommentGroupPosts.Dto;

public class LikeCommentGroupPostDto
{
	public Guid Id { get; set; }
	public Guid UserId { get; set; }
	public string UserName { get; set; }
	public string UserAvatar { get; set; }
	public Guid CommentGroupPostId { get; set; }
	public DateTime CreatedAt { get; set; }
}

