using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistences.Configuration;

public class CertificationConfiguration : IEntityTypeConfiguration<Certification>
{
	public void Configure(EntityTypeBuilder<Certification> builder)
	{
		builder.ToTable("Certifications");
		builder.HasKey(c => c.Id);
		builder.Property(c => c.Id)
		.HasDefaultValueSql("NEWID()");
		builder.Property(c => c.UserId).IsRequired();
		builder.Property(c => c.Title).IsRequired().HasMaxLength(255);
		builder.Property(c => c.IssuingAuthority).HasMaxLength(255);
		builder.Property(c => c.CreatedAt).HasDefaultValueSql("GETDATE()");
		builder.HasOne(c => c.User)
			   .WithMany(u => u.Certifications)
			   .HasForeignKey(c => c.UserId)
			   .OnDelete(DeleteBehavior.Restrict);
	}
}
