using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class CommentGroupPost : BaseEntity
{
	public Guid GroupPostId { get; set; }
	public Guid UserId { get; set; }
	public string Content { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public Guid? ParentCommentId { get; set; }

	public User User { get; set; } // Navigation property to the User who commented
	public CommentGroupPost ParentComment { get; set; } // Navigation property for nested comments
	public GroupPost GroupPost { get; set; } // Navigation property to the parent GroupPost
	public ICollection<CommentGroupPost> ChildComments { get; set; } // Navigation property for child comments
}
