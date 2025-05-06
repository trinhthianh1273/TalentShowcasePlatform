using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Payments.Dto;
public class PaymentDto
{
	public Guid Id { get; set; }
	public Guid SenderId { get; set; }
	public Guid ReceiverId { get; set; }
	public decimal Amount { get; set; }
	public string Message { get; set; }
	public DateTime CreatedAt { get; set; }

	// Navigation Properties DTOs (if needed)
	// public UserDto Sender { get; set; }
	// public UserDto Receiver { get; set; }
}
