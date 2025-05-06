using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistences.Configuration;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
	public void Configure(EntityTypeBuilder<Comment> builder)
	{
		builder.ToTable("Comments");
		builder.HasKey(c => c.Id);
		builder.Property(c => c.Id)
		.HasDefaultValueSql("NEWID()");
		builder.HasOne(c => c.Video)
			   .WithMany(v => v.Comments)
			   .HasForeignKey(c => c.VideoId);
		builder.HasOne(c => c.User)
			   .WithMany(u => u.Comments)
			   .HasForeignKey(c => c.UserId)
			   .OnDelete(DeleteBehavior.Cascade);
		builder.Property(c => c.Content).HasMaxLength(1000);
		builder.Property(c => c.CreatedAt).HasDefaultValueSql("GETDATE()");
	}
}
