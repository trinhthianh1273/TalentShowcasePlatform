using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GroupMembers.Dto;

public class GroupMemberDto
{
	public Guid Id { get; set; }
	public Guid GroupId { get; set; }
	public Guid UserId { get; set; }
	public DateTime JoinedAt { get; set; }

	// Navigation Properties DTOs (if needed)
	// public GroupDto Group { get; set; }
	// public UserDto User { get; set; }
}
