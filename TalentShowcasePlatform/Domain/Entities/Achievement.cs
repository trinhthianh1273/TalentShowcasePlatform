using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Achievement : BaseEntity
{
	public Guid UserId { get; set; }
	public string Title { get; set; }
	public string? Description { get; set; }
	public DateTime? DateAchieved { get; set; }
	public DateTime CreatedAt { get; set; }

	// Navigation Properties
	public User User { get; set; }
}
