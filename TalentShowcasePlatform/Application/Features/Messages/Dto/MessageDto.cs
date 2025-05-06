using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Messages.Dto;

public class MessageDto
{
	public Guid Id { get; set; }
	public Guid SenderId { get; set; }
	public Guid ReceiverId { get; set; }
	public string Content { get; set; }
	public DateTime SentAt { get; set; }

	// Navigation Properties DTOs (if needed)
	// public UserDto Sender { get; set; }
	// public UserDto Receiver { get; set; }
}
