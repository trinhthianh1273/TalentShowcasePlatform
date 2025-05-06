using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Jobs.Dto;

public class JobDto
{
	public Guid Id { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public Guid PostedBy { get; set; }
	public Guid CategoryId { get; set; }
	public string Location { get; set; }
	public string JobType { get; set; }
	public decimal? SalaryFrom { get; set; }
	public decimal? SalaryTo { get; set; }
	public DateTime? ExpiryDate { get; set; }
	public string ContactEmail { get; set; }
	public DateTime CreatedAt { get; set; }

	// Navigation properties (if needed)
	// public UserDto PostedByUser { get; set; }
	// public CategoryDto Category { get; set; }
}
