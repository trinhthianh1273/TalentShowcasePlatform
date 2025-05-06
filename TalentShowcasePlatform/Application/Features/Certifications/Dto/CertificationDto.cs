using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Certifications.Dto;

public class CertificationDto
{
	public Guid Id { get; set; }
	public Guid UserId { get; set; }
	public string Title { get; set; }
	public string IssuingAuthority { get; set; }
	public DateTime? IssueDate { get; set; }
	public DateTime? ExpiryDate { get; set; }
	public DateTime CreatedAt { get; set; }

	// Navigation Property DTO
	public User User { get; set; }
}
