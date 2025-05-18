using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;

namespace Infrastructure.Persistences.Configuration;

public class CommentGroupPostConfiguration : IEntityTypeConfiguration<CommentGroupPost>
{
	public void Configure(EntityTypeBuilder<CommentGroupPost> builder)
	{
		builder.ToTable("CommentGroupPosts");
		builder.HasKey(cgp => cgp.Id);

		builder.Property(cgp => cgp.Content)
			.IsRequired();

		builder.Property(cgp => cgp.CreatedAt)
			.IsRequired();

		// Relationship with GroupPost
		builder.HasOne(cgp => cgp.GroupPost)
			.WithMany(gp => gp.Comments) // Assuming GroupPost has a collection named Comments
			.HasForeignKey(cgp => cgp.GroupPostId)
			.OnDelete(DeleteBehavior.Cascade); // When a GroupPost is deleted, its comments are also deleted

		// Quan hệ giữa CommentGroupPost và User
		builder.HasOne(c => c.User)
			.WithMany(u => u.CommentGroupPosts)
			.HasForeignKey(c => c.UserId)
			.OnDelete(DeleteBehavior.Restrict);

		// Self-referencing relationship for nested comments
		builder.HasOne(cgp => cgp.ParentComment)
			.WithMany(cgp => cgp.ChildComments)
			.HasForeignKey(cgp => cgp.ParentCommentId)
			.OnDelete(DeleteBehavior.Restrict); // Prevent deleting a parent comment if it has child comments

		// Indexes for performance
		builder.HasIndex(cgp => cgp.GroupPostId);
		builder.HasIndex(cgp => cgp.UserId);
		builder.HasIndex(cgp => cgp.ParentCommentId);
		builder.HasIndex(cgp => cgp.CreatedAt);
	}
}
