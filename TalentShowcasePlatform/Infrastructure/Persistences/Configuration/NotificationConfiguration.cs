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

		builder.Property(n => n.Title)
			.IsRequired()
			.HasMaxLength(200);

		builder.Property(n => n.Message)
			.HasMaxLength(1000);

		builder.Property(n => n.Type)
			.IsRequired()
			.HasMaxLength(100);

		builder.Property(n => n.RelatedEntityType)
			.HasConversion<string>()
			.IsUnicode(false)
			.HasMaxLength(50)
			.IsRequired(false);

		builder.Property(n => n.IsRead)
			.HasDefaultValue(false);

		builder.Property(n => n.CreatedAt)
			.HasDefaultValueSql("GETUTCDATE()");

		builder.HasOne(n => n.User)
			.WithMany(u => u.Notifications)
			.HasForeignKey(n => n.UserId)
			.OnDelete(DeleteBehavior.Cascade); // Nếu User bị xóa, xóa theo Notification
	}
}
