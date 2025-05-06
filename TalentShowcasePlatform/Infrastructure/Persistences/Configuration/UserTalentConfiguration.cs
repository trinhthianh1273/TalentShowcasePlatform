using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistences.Configuration;

public class UserTalentConfiguration : IEntityTypeConfiguration<UserTalent>
{
	public void Configure(EntityTypeBuilder<UserTalent> builder)
	{
		builder.ToTable("UserTalents");
		builder.HasKey(ut => new { ut.UserId, ut.CategoryId });
		builder.Property(ut => ut.SkillLevel)
				.HasConversion<string>();

		builder.HasOne(ut => ut.User)
			   .WithMany(u => u.UserTalents)
			   .HasForeignKey(ut => ut.UserId)
			   .OnDelete(DeleteBehavior.Restrict);
		builder.HasOne(ut => ut.Category)
			   .WithMany(c => c.UserTalents)
			   .HasForeignKey(ut => ut.CategoryId)
			   .OnDelete(DeleteBehavior.Restrict);
	}
}
