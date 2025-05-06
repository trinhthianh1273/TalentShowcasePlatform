using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Dto;

public class UserNavigationDto
{
	public Guid Id { get; set; }
	public string UserName { get; set; }
	public string Email { get; set; }
	public string Bio { get; set; }
	public Role Role { get; set; }
}
