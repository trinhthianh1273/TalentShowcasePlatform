using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistences.Configuration;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
	public void Configure(EntityTypeBuilder<Notification> builder)
	{
		builder.ToTable("Notifications");
		builder.HasKey(n => n.Id);
		builder.Property(n => n.Id)
		.HasDefaultValueSql("NEWID()");
		builder.HasOne(n => n.User)
			   .WithMany(u => u.Notifications)
			   .HasForeignKey(n => n.UserId)
			   .OnDelete(DeleteBehavior.Restrict);
		builder.Property(n => n.Message).HasMaxLength(500);
		builder.Property(n => n.IsRead).IsRequired();
		builder.Property(n => n.CreatedAt).HasDefaultValueSql("GETDATE()");
	}
}
