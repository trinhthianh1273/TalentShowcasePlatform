using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class View : BaseEntity
{
	public Guid VideoId { get; set; }
	public Guid ViewerId { get; set; }
	public DateTime ViewedAt { get; set; }

	// Navigation Properties
	public Video Video { get; set; }
	public User Viewer { get; set; }
}
