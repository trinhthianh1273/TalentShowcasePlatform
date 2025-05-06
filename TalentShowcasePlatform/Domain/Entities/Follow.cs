using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Follow : BaseEntity
{
	public Guid FollowingUserId { get; set; }
	public Guid FollowedUserId { get; set; }

	public DateTime CreatedAt { get; set; }

	// Navigation Properties
	public User FollowingUser { get; set; }
	public User FollowedUser { get; set; }
}
