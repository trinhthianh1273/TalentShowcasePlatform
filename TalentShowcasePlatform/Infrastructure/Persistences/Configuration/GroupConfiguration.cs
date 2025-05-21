using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistences.Configuration;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
	public void Configure(EntityTypeBuilder<Group> builder)
	{
		builder.ToTable("Groups");
		builder.HasKey(g => g.Id);
		builder.Property(c => c.Id)
		.HasDefaultValueSql("NEWID()");
		builder.Property(g => g.Name).HasMaxLength(100);
		builder.Property(g => g.Description).HasMaxLength(500);
		builder.Property(g => g.GroupAvatar).HasMaxLength(500);
		builder.Property(g => g.CategoryId).HasMaxLength(50);

		builder.HasOne(g => g.CreatedByUser)
			   .WithMany(u => u.CreatedGroups)
			   .HasForeignKey(g => g.CreatedBy)
			   .OnDelete(DeleteBehavior.Restrict);

		builder.HasOne(g => g.Category)
			   .WithMany(u => u.Groups)
			   .HasForeignKey(g => g.CategoryId)
			   .OnDelete(DeleteBehavior.Restrict);
		builder.Property(g => g.CreatedAt).HasDefaultValueSql("GETDATE()");
	}
}
