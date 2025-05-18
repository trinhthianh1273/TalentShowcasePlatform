using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class GroupPost : BaseEntity
{
	public Guid GroupId { get; set; }
	public Group Group { get; set; } // Navigation property to the Group entity

	public string Content { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public DateTime? LastActivityDate { get; set; }
	public string ImgUrl { get; set; }

	public Guid UserId { get; set; }
	public User User { get; set; } // Navigation property to the User entity
	// Navigation property for comments
	public ICollection<CommentGroupPost> Comments { get; set; } = new List<CommentGroupPost>(); // Initialize to avoid null reference exceptions
}
