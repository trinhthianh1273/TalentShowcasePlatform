using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class GroupPostFeedbackSummary : BaseEntity
{
	public Guid GroupPostId { get; set; }
	public GroupPost GroupPost { get; set; }

	public int TotalLikes { get; set; }
	public int TotalComments { get; set; }
	public double AverageRating { get; set; }
	public int TotalRatings { get; set; }
}
