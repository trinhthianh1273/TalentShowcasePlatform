using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Dto;

public class AuthenticationResponseDto
{
	public Guid UserId { get; set; }
	public string UserName { get; set; }
	public string Email { get; set; }
	public string Token { get; set; }
}
