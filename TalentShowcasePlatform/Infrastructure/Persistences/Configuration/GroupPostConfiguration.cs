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
		builder.HasKey(gp => gp.Id);

		builder.Property(gp => gp.Content)
			   .IsRequired()
			   .HasMaxLength(1000);

		builder.Property(gp => gp.CreatedAt)
			   .HasDefaultValueSql("GETDATE()");

		builder.HasOne(gp => gp.User)
			   .WithMany(u => u.GroupPosts)
			   .HasForeignKey(gp => gp.UserId)
			   .OnDelete(DeleteBehavior.Cascade);

		builder.HasOne(gp => gp.Group)
			   .WithMany(g => g.GroupPosts)
			   .HasForeignKey(gp => gp.GroupId)
			   .OnDelete(DeleteBehavior.Cascade);
	}
}
