using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ContestEntries.Dto;

public class ContestEntryDto
{
	public Guid Id { get; set; }
	public Guid ContestId { get; set; }
	public Guid VideoId { get; set; }
	public DateTime SubmittedAt { get; set; }
	public int Votes { get; set; }

	// Navigation Properties DTOs (if needed)
	// public ContestDto Contest { get; set; }
	// public VideoDto Video { get; set; }
}