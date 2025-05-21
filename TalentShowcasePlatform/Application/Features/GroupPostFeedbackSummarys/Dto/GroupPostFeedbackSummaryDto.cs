using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GroupPostFeedbackSummarys.Dto;

public class GroupPostFeedbackSummaryDto
{
	public Guid GroupPostId { get; set; }
	public int TotalLikes { get; set; }
	public int TotalComments { get; set; }
	public double AverageRating { get; set; }
	public int TotalRatings { get; set; }
}

