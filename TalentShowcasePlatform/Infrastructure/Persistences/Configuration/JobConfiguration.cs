using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistences.Configuration;

public class JobConfiguration : IEntityTypeConfiguration<Job>
{
	public void Configure(EntityTypeBuilder<Job> builder)
	{
		builder.ToTable("Jobs");
		builder.HasKey(j => j.Id);
		builder.Property(j => j.Id)
		.HasDefaultValueSql("NEWID()");
		builder.Property(j => j.Title).HasMaxLength(200);
		builder.Property(j => j.Description).HasMaxLength(1000);
		builder.Property(j => j.SalaryFrom)
				.HasPrecision(18, 0);
		builder.Property(j => j.SalaryTo)
				.HasPrecision(18, 0);
		builder.HasOne(j => j.PostedByUser)
			   .WithMany(u => u.PostedJobs)
			   .HasForeignKey(j => j.PostedBy)
			   .OnDelete(DeleteBehavior.Restrict);
		builder.Property(j => j.CreatedAt).HasDefaultValueSql("GETDATE()");
	}
}
