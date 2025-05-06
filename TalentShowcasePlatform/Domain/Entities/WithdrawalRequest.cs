using Domain.Common;
using Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class WithdrawalRequest : BaseEntity
{
	public Guid UserId { get; set; }
	public decimal Amount { get; set; }
	public WithdrawalStatus Status { get; set; }
	public DateTime RequestedAt { get; set; }
	public string? Note { get; set; }

	public virtual User User { get; set; }
}
