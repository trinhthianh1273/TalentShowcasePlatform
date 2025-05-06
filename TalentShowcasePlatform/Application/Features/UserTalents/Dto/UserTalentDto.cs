using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserTalents.Dto;

public class UserTalentDto
{
	public Guid Id { get; set; }
	public Guid UserId { get; set; }
	public Guid CategoryId { get; set; }

	// Navigation Properties DTOs (if needed)
	// public UserDto User { get; set; }
	// public CategoryDto Category { get; set; }
}
