using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistences.Configuration;

public class LikeCommentGroupPostConfiguration : IEntityTypeConfiguration<LikeCommentGroupPost>
{
	public void Configure(EntityTypeBuilder<LikeCommentGroupPost> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(x => x.CreatedAt)
			.HasDefaultValueSql("GETUTCDATE()");

		builder.HasOne(x => x.CommentGroupPost)
			.WithMany(c => c.Likes)
			.HasForeignKey(x => x.CommentGroupPostId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasOne(x => x.User)
			.WithMany(x => x.LikeCommentGroupPosts)
			.HasForeignKey(x => x.UserId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasIndex(x => new { x.UserId, x.CommentGroupPostId }).IsUnique();
	}
}

