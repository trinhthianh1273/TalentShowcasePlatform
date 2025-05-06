using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistences.Configuration;

public class ViewConfiguration : IEntityTypeConfiguration<View>
{
	public void Configure(EntityTypeBuilder<View> builder)
	{
		builder.ToTable("Views");
		builder.HasKey(v => v.Id);
		builder.Property(v => v.Id)
		.HasDefaultValueSql("NEWID()");
		builder.HasOne(v => v.Video)
			   .WithMany(vi => vi.Views)
			   .HasForeignKey(v => v.VideoId)
			   .OnDelete(DeleteBehavior.Restrict);
		builder.HasOne(v => v.Viewer)
			   .WithMany(u => u.Views)
			   .HasForeignKey(v => v.ViewerId)
			   .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete
		builder.Property(v => v.ViewedAt).HasDefaultValueSql("GETDATE()");
	}
}
