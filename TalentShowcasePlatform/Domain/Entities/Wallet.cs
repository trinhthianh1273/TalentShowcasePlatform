using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Wallet : BaseEntity
{
	public Guid UserId { get; set; }
	public decimal Balance { get; set; }
	public DateTime LastUpdated { get; set; }

	public virtual User User { get; set; }
}