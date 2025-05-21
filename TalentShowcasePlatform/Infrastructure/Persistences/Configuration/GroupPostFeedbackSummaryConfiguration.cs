using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistences.Configuration;

public class GroupPostFeedbackSummaryConfiguration : IEntityTypeConfiguration<GroupPostFeedbackSummary>
{
	public void Configure(EntityTypeBuilder<GroupPostFeedbackSummary> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(x => x.TotalLikes).HasDefaultValue(0);
		builder.Property(x => x.TotalComments).HasDefaultValue(0);
		builder.Property(x => x.AverageRating).HasDefaultValue(0);
		builder.Property(x => x.TotalRatings).HasDefaultValue(0);

		builder.HasOne(x => x.GroupPost)
			.WithOne(p => p.FeedbackSummary)
			.HasForeignKey<GroupPostFeedbackSummary>(x => x.GroupPostId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasIndex(x => x.GroupPostId).IsUnique();
	}
}

