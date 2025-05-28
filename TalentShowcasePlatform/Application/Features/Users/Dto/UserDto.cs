using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Dto;

public class UserDto
{
	public Guid Id { get; set; }
	public string UserName { get; set; }
	public string FullName { get; set; }
	public string Email { get; set; }
	public string Bio { get; set; }
	public string AvatarUrl { get; set; }
	public string Location { get; set; }
	public Guid RoleId { get; set; }
	public string RoleName { get; set; }
	public DateTime CreatedAt { get; set; }
}
