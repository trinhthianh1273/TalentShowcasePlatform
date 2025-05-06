using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistences.Configuration;

public class VideoConfiguration : IEntityTypeConfiguration<Video>
{
	public void Configure(EntityTypeBuilder<Video> builder)
	{
		builder.ToTable("Videos");
		builder.HasKey(v => v.Id);
		builder.Property(v => v.Id)
		.HasDefaultValueSql("NEWID()");
		builder.Property(v => v.Title).HasMaxLength(200);
		builder.Property(v => v.Description).HasMaxLength(500);
		builder.Property(v => v.Url).HasMaxLength(500);
		builder.HasOne(v => v.User)
			   .WithMany(u => u.Videos)
			   .HasForeignKey(v => v.UserId)
			   .OnDelete(DeleteBehavior.Restrict);
		builder.HasOne(v => v.Category)
			   .WithMany(c => c.Videos)
			   .HasForeignKey(v => v.CategoryId)
			   .OnDelete(DeleteBehavior.Restrict);
		builder.Property(v => v.IsPrivate).IsRequired(); // Default is false, but be explicit
		builder.Property(v => v.UploadedAt).HasDefaultValueSql("GETDATE()");

	}
}
