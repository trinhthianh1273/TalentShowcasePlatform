using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistences.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.ToTable("Users");
		builder.HasKey(u => u.Id);
		builder.Property(u => u.Id)
		.HasDefaultValueSql("NEWID()");
		builder.Property(u => u.UserName).HasMaxLength(100);
		builder.Property(u => u.Email).HasMaxLength(100);
		builder.Property(u => u.PasswordHash).IsRequired();
		builder.Property(u => u.Bio).HasMaxLength(1000);
		builder.Property(u => u.AvatarUrl).HasMaxLength(500);
		builder.Property(u => u.Location).HasMaxLength(500);
		builder.HasOne(u => u.Role)
			   .WithMany(r => r.Users)
			   .HasForeignKey(u => u.RoleId)
			   .OnDelete(DeleteBehavior.Restrict);
		builder.Property(u => u.CreatedAt).HasDefaultValueSql("GETDATE()");
	}
}
