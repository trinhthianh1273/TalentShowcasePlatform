using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Groups.Dto;

public class GroupDto
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public Guid CreatedBy { get; set; }
	public DateTime CreatedAt { get; set; }

	// Navigation Properties DTOs (if needed)
	// public UserDto CreatedByUser { get; set; }
	// public ICollection<GroupMemberDto> GroupMembers { get; set; }
}
