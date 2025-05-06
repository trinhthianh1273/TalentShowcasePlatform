using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistences.Configuration;

public class ContestConfiguration : IEntityTypeConfiguration<Contest>
{
	public void Configure(EntityTypeBuilder<Contest> builder)
	{
		builder.ToTable("Contests");
		builder.HasKey(c => c.Id);
		builder.Property(c => c.Id)
		.HasDefaultValueSql("NEWID()");
		builder.Property(c => c.Title).HasMaxLength(200);
		builder.Property(c => c.Description).HasMaxLength(1000);
		builder.HasOne(c => c.CreatedByUser)
			   .WithMany()
			   .HasForeignKey(c => c.CreatedBy)
			   .IsRequired()
			   .OnDelete(DeleteBehavior.Restrict);
		builder.Property(c => c.StartDate).IsRequired();
		builder.Property(c => c.EndDate).IsRequired();
	}
}
