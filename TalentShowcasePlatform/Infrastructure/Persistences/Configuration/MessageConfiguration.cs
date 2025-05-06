using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistences.Configuration;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
	public void Configure(EntityTypeBuilder<Message> builder)
	{
		builder.ToTable("Messages");
		builder.HasKey(m => m.Id);
		builder.Property(m => m.Id)
		.HasDefaultValueSql("NEWID()");
		builder.HasOne(m => m.Sender)
			   .WithMany(u => u.SentMessages)
			   .HasForeignKey(m => m.SenderId)
			   .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete
		builder.HasOne(m => m.Receiver)
			   .WithMany(u => u.ReceivedMessages)
			   .HasForeignKey(m => m.ReceiverId)
			   .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete
		builder.Property(m => m.Content).HasMaxLength(1000);
		builder.Property(m => m.SentAt).HasDefaultValueSql("GETDATE()");
	}
}
