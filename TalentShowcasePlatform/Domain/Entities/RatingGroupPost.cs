using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class RatingGroupPost : BaseEntity
{
	public Guid GroupPostId { get; set; }
	public GroupPost GroupPost { get; set; }

	public Guid UserId { get; set; }
	public User User { get; set; }

	public int Value { get; set; } // 1-5 sao
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
