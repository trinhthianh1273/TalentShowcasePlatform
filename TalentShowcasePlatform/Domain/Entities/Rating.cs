using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Rating : BaseEntity
{
	public Guid VideoId { get; set; }
	public Guid UserId { get; set; }
	public int Stars { get; set; }

	// Navigation Properties
	public Video Video { get; set; }
	public User User { get; set; }
}
