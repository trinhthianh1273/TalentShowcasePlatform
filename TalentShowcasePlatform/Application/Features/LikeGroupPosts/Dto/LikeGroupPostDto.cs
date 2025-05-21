using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LikeGroupPosts.Dto;

public class LikeGroupPostDto
{
	public Guid Id { get; set; }
	public Guid UserId { get; set; }
	public string UserName { get; set; }
	public string UserAvatar { get; set; }
	public Guid GroupPostId { get; set; }
	public DateTime CreatedAt { get; set; }
}

