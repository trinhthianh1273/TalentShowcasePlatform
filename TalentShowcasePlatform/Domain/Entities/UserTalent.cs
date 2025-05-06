using Domain.Common;
using Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class UserTalent : BaseEntity
{
	public Guid UserId { get; set; }
	public Guid CategoryId { get; set; }
	public SkillLevel SkillLevel { get; set; }

	// Navigation Properties
	public User User { get; set; }
	public Category Category { get; set; }
}
