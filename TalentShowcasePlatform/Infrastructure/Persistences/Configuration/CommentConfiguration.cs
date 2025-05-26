using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistences.Configuration;

public class CommentVideoConfiguration : IEntityTypeConfiguration<CommentVideo>
{
	public void Configure(EntityTypeBuilder<CommentVideo> builder)
	{
		builder.ToTable("CommentVideos");
		builder.HasKey(c => c.Id);
		builder.Property(c => c.Id)
		.HasDefaultValueSql("NEWID()");
		builder.HasOne(c => c.Video)
			   .WithMany(v => v.CommentVideos)
			   .HasForeignKey(c => c.VideoId)
			   .OnDelete(DeleteBehavior.Cascade);
		builder.HasOne(c => c.User)
			   .WithMany(u => u.CommentVideos)
			   .HasForeignKey(c => c.UserId)
			   .OnDelete(DeleteBehavior.Cascade);
		builder.Property(c => c.Content).HasMaxLength(1000);
		builder.Property(c => c.CreatedAt).HasDefaultValueSql("GETDATE()");
	}
}
