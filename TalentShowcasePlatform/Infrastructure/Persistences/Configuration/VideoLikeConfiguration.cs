using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistences.Configuration;

public class VideoLikeConfiguration : IEntityTypeConfiguration<VideoLike>
{
	public void Configure(EntityTypeBuilder<VideoLike> builder)
	{
		builder.ToTable("VideoLikes");
		builder.HasKey(vl => vl.Id);
			builder.Property(vl => vl.Id)
			.HasDefaultValueSql("NEWID()");
		builder.HasOne(vl => vl.Video)
			   .WithMany(v => v.VideoLikes)
			   .HasForeignKey(vl => vl.VideoId)
			   .OnDelete(DeleteBehavior.Restrict);
		builder.HasOne(vl => vl.User)
			   .WithMany(u => u.VideoLikes)
			   .HasForeignKey(vl => vl.UserId)
			   .OnDelete(DeleteBehavior.Restrict);
		builder.Property(vl => vl.LikedAt).HasDefaultValueSql("GETDATE()");
	}
}
