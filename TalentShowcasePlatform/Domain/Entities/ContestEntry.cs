using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class ContestEntry : BaseEntity
{
	public Guid ContestId { get; set; }
	public Guid VideoId { get; set; }
	public DateTime SubmittedAt { get; set; }
	public int Votes { get; set; }

	// Navigation Properties
	public Contest Contest { get; set; }
	public Video Video { get; set; }
}
