using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Awards.Query;

public class AwardDto
{
	public Guid Id { get; set; }
	public Guid UserId { get; set; }
	public string Title { get; set; }
	public string AwardingOrganization { get; set; }
	public DateTime? DateReceived { get; set; }
	public DateTime CreatedAt { get; set; }

	// Navigation Property DTO
	public User User { get; set; }
}
