using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistences.Configuration;

public class LikeGroupPostConfiguration : IEntityTypeConfiguration<LikeGroupPost>
{
	public void Configure(EntityTypeBuilder<LikeGroupPost> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(x => x.CreatedAt)
			.HasDefaultValueSql("GETUTCDATE()");

		builder.HasOne(x => x.GroupPost)
			.WithMany(p => p.Likes)
			.HasForeignKey(x => x.GroupPostId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasOne(x => x.User)
			.WithMany(x => x.LikeGroupPosts)
			.HasForeignKey(x => x.UserId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasIndex(x => new { x.UserId, x.GroupPostId }).IsUnique();
	}
}
