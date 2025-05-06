using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistences.Configuration;

public class ContestEntryConfiguration : IEntityTypeConfiguration<ContestEntry>
{
	public void Configure(EntityTypeBuilder<ContestEntry> builder)
	{
		builder.ToTable("ContestEntries");
		builder.HasKey(ce => ce.Id);
		builder.Property(c => c.Id)
		.HasDefaultValueSql("NEWID()");
		builder.HasOne(ce => ce.Contest)
			   .WithMany(c => c.ContestEntries)
			   .HasForeignKey(ce => ce.ContestId)
			   .OnDelete(DeleteBehavior.Restrict);
		builder.HasOne(ce => ce.Video)
			   .WithMany(v => v.ContestEntries)
			   .HasForeignKey(ce => ce.VideoId)
			   .OnDelete(DeleteBehavior.Restrict);
		builder.Property(ce => ce.SubmittedAt).HasDefaultValueSql("GETDATE()");
		builder.Property(ce => ce.Votes).IsRequired();
	}
}
