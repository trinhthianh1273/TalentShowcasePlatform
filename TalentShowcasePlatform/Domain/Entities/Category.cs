using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Category : BaseEntity
{
	public string Name { get; set; }
	public string Description { get; set; }

	// Navigation Properties
	public ICollection<UserTalent> UserTalents { get; set; }
	public ICollection<Video> Videos { get; set; }
}
