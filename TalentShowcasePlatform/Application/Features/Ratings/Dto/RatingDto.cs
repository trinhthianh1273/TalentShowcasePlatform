using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Ratings.Dto;

public class RatingDto
{
	public Guid Id { get; set; }
	public Guid VideoId { get; set; }
	public Guid UserId { get; set; }
	public int Stars { get; set; }

	// Navigation Properties DTOs (if needed)
	// public VideoDto Video { get; set; }
	// public UserDto User { get; set; }
}
