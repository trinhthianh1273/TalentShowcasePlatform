using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistences.Configuration;

public class GroupPostConfiguration : IEntityTypeConfiguration<GroupPost>
{
	public void Configure(EntityTypeBuilder<GroupPost> builder)
	{
		// Table name
		builder.ToTable("GroupPosts");

		// Primary Key
		builder.HasKey(gp => gp.Id);

		// Properties
		builder.Property(gp => gp.Title)
			   .IsRequired()
			   .HasMaxLength(1000);

		// Properties
		builder.Property(gp => gp.Content)
			   .IsRequired();

		builder.Property(gp => gp.CreatedAt)
			   .IsRequired()
			   .HasDefaultValueSql("GETDATE()");

		builder.Property(gp => gp.ImgUrl)
			   .HasMaxLength(255);

		builder.Property(gp => gp.LastActivityDate)
			   .IsRequired(false);

		// Indexes
		builder.HasIndex(gp => gp.GroupId);
		builder.HasIndex(gp => gp.UserId);
		builder.HasIndex(gp => gp.CreatedAt);
		builder.HasIndex(gp => gp.LastActivityDate);

		// Relationships

		// GroupPost → Group (nhiều bài viết thuộc 1 group)
		builder.HasOne(gp => gp.Group)
			   .WithMany(g => g.GroupPosts)
			   .HasForeignKey(gp => gp.GroupId)
			   .OnDelete(DeleteBehavior.Cascade);

		// GroupPost → User (nhiều bài viết do 1 user tạo)
		builder.HasOne(gp => gp.User)
			   .WithMany(u => u.GroupPosts)
			   .HasForeignKey(gp => gp.UserId)
			   .OnDelete(DeleteBehavior.Cascade);

		// GroupPost → CommentGroupPost (quan hệ 1-nhiều ngược lại được khai báo ở CommentGroupPost)
		// Không cần cấu hình thêm ở đây nếu đã có ở CommentGroupPost
	}
}

