using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Achievements.Query;

public class AchievementDto
{
	public Guid Id { get; set; }
	public Guid UserId { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public DateTime? DateAchieved { get; set; }
	public DateTime CreatedAt { get; set; }

	// Navigation Property DTO (nếu bạn muốn trả về thông tin User)
	//public User User { get; set; }
}
