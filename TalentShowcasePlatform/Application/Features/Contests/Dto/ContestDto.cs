using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Contests.Dto;

public class ContestDto
{
	public Guid Id { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public Guid CreatedBy { get; set; }
	public DateTime StartDate { get; set; }
	public DateTime EndDate { get; set; }

	// Navigation Properties DTOs (if needed)
	// public UserDto CreatedByUser { get; set; }
	// public ICollection<ContestEntryDto> ContestEntries { get; set; }
}
