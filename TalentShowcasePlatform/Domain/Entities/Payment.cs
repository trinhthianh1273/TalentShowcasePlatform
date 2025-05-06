using Domain.Common;
using Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Payment : BaseEntity
{
	public Guid SenderId { get; set; }
	public Guid ReceiverId { get; set; }
	public decimal Amount { get; set; }
	public string Message { get; set; }
	public PaymentType Type { get; set; }
	public bool IsProcessed { get; set; } = false;
	public DateTime CreatedAt { get; set; }

	// Navigation Properties
	public User Sender { get; set; }
	public User Receiver { get; set; }
}
