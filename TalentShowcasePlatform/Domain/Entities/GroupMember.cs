using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class GroupMember : BaseEntity
{
	public Guid GroupId { get; set; }
	public Guid UserId { get; set; }
	public DateTime JoinedAt { get; set; }

	// Navigation Properties
	public Group Group { get; set; }
	public User User { get; set; }
}

