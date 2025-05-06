using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class GroupPost : BaseEntity
{
	public Guid GroupId { get; set; }
	public Guid UserId { get; set; }
	public string Content { get; set; }
	public DateTime CreatedAt { get; set; }

	public virtual Group Group { get; set; }
	public virtual User User { get; set; }
}
