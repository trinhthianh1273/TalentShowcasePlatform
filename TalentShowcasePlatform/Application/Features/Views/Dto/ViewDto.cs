using MediatR;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Views.Dto;

public class ViewDto
{
	public Guid Id { get; set; }
	public Guid VideoId { get; set; }
	public Guid ViewerId { get; set; }
	public DateTime ViewedAt { get; set; }

	// Navigation Properties DTOs (if needed)
	// public VideoDto Video { get; set; }
	// public UserDto Viewer { get; set; }
}
