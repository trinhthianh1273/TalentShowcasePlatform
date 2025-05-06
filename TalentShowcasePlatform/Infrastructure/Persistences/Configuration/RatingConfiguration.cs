using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistences.Configuration;

public class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
	public void Configure(EntityTypeBuilder<Rating> builder)
	{
		builder.ToTable("Ratings");
		builder.HasKey(r => r.Id);
		builder.Property(r => r.Id)
		.HasDefaultValueSql("NEWID()");
		builder.HasOne(r => r.Video)
			   .WithMany(v => v.Ratings)
			   .HasForeignKey(r => r.VideoId)
			   .OnDelete(DeleteBehavior.Restrict);
		builder.HasOne(r => r.User)
			   .WithMany(u => u.Ratings)
			   .HasForeignKey(r => r.UserId)
			   .OnDelete(DeleteBehavior.Restrict);
		builder.Property(r => r.Stars).IsRequired();
	}
}
