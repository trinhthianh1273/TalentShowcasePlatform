using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Contest : BaseEntity
{
	public string Title { get; set; }
	public string Description { get; set; }
	public Guid CreatedBy { get; set; }
	public DateTime StartDate { get; set; }
	public DateTime EndDate { get; set; }

	// Navigation Properties
	public User CreatedByUser { get; set; }
	public ICollection<ContestEntry> ContestEntries { get; set; }
}
