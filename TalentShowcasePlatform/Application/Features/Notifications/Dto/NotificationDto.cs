using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Notifications.Dto;

public class NotificationDto
{
	public Guid Id { get; set; }
	public Guid UserId { get; set; }
	public string Message { get; set; }
	public bool IsRead { get; set; }
	public DateTime CreatedAt { get; set; }

	// Navigation Properties DTOs (if needed)
	// public UserDto User { get; set; }
}
