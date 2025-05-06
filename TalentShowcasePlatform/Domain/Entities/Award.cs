using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Award : BaseEntity

{
	public Guid UserId { get; set; }
	public string Title { get; set; }
	public string AwardingOrganization { get; set; }
	public DateTime? DateReceived { get; set; }
	public DateTime CreatedAt { get; set; }

	// Navigation Properties
	public User User { get; set; }
}
