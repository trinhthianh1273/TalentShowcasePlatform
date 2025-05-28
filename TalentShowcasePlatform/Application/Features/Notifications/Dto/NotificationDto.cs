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

	public string Title { get; set; }
	public string Message { get; set; }
	public string Type { get; set; }

	public Guid? RelatedEntityId { get; set; }
	public string RelatedEntityType { get; set; }

	public bool IsRead { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime? ReadAt { get; set; }
}
