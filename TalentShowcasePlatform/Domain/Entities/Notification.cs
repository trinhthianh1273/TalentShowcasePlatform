using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Notification : BaseEntity
{
	public Guid UserId { get; set; }
	public string Message { get; set; }
	public bool IsRead { get; set; }
	public DateTime CreatedAt { get; set; }

	// Navigation Properties
	public User User { get; set; }
}
