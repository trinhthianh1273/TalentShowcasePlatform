using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Group : BaseEntity
{
	public string Name { get; set; }
	public string Description { get; set; }
	public Guid CreatedBy { get; set; }
	public DateTime CreatedAt { get; set; }

	// Navigation Properties
	public User CreatedByUser { get; set; }
	public ICollection<GroupMember> GroupMembers { get; set; }
	public ICollection<GroupPost> GroupPosts { get; set; }
}
