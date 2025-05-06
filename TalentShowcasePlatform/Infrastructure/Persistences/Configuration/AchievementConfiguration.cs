using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistences.Configuration;

public class AchievementConfiguration : IEntityTypeConfiguration<Achievement>
{
	public void Configure(EntityTypeBuilder<Achievement> builder)
	{
		builder.ToTable("Achievements");
		builder.HasKey(a => a.Id);
		builder.Property(a => a.Id)
		.HasDefaultValueSql("NEWID()");
		builder.Property(a => a.UserId).IsRequired();
		builder.Property(a => a.Title).IsRequired().HasMaxLength(255);
		builder.Property(a => a.Description).HasMaxLength(1000);
		builder.Property(a => a.CreatedAt).HasDefaultValueSql("GETDATE()");
		builder.HasOne(a => a.User)
			   .WithMany(u => u.Achievements)
			   .HasForeignKey(a => a.UserId)
			   .OnDelete(DeleteBehavior.Restrict);
	}
}
