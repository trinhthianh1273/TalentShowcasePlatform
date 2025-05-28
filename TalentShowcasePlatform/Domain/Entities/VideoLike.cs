using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class VideoLike : BaseEntity
{
	public Guid VideoId { get; set; }
	public Guid UserId { get; set; }
	public DateTime LikedAt { get; set; } = DateTime.Now;

	// Navigation Properties
	public Video Video { get; set; }
	public User User { get; set; }
}
