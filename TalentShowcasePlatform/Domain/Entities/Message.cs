using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Message : BaseEntity
{
	public Guid SenderId { get; set; }
	public Guid ReceiverId { get; set; }
	public string Content { get; set; }
	public DateTime SentAt { get; set; }

	// Navigation Properties
	public User Sender { get; set; }
	public User Receiver { get; set; }
}
