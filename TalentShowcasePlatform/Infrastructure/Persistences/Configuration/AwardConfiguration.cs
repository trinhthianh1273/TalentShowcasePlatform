using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistences.Configuration;

public class AwardConfiguration : IEntityTypeConfiguration<Award>
{
	public void Configure(EntityTypeBuilder<Award> builder)
	{
		builder.ToTable("Awards");
		builder.HasKey(a => a.Id);
		builder.Property(a => a.Id)
		.HasDefaultValueSql("NEWID()");
		builder.Property(a => a.UserId).IsRequired();
		builder.Property(a => a.Title).IsRequired().HasMaxLength(255);
		builder.Property(a => a.AwardingOrganization).HasMaxLength(255);
		builder.Property(a => a.CreatedAt).HasDefaultValueSql("GETDATE()");
		builder.HasOne(a => a.User)
			   .WithMany(u => u.Awards)
			   .HasForeignKey(a => a.UserId)
			   .OnDelete(DeleteBehavior.Restrict);
	}
}
