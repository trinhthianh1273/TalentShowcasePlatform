using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Roles.Dto;

public class RoleDto
{
	public Guid Id { get; set; }
	public string Name { get; set; }

	// Navigation Properties DTOs (if needed - be cautious of circular references)
	//public ICollection<UserDto> Users { get; set; }
}
