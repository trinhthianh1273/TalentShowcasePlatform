using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Jobs.Dto;

public class JobShortDto
{
	public Guid Id { get; set; }
	public string Title { get; set; }
	public Guid CategoryId { get; set; }
	public string CategoryName { get; set; }
	public string UserName { get; set; }
	public string UserAvatarUrl { get; set; }
	public string Location { get; set; }
	public Guid PostedBy { get; set; }
	public DateTime CreatedAt { get; set; }
}
