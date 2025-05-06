using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistences.Configuration;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
	public void Configure(EntityTypeBuilder<Payment> builder)
	{
		builder.ToTable("Payments");
		builder.HasKey(p => p.Id);
		builder.Property(p => p.Id)
		.HasDefaultValueSql("NEWID()");
		builder.Property(p => p.Type)
				.HasConversion<string>();
		builder.HasOne(p => p.Sender)
			   .WithMany(u => u.SentPayments)
			   .HasForeignKey(p => p.SenderId)
			   .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

		builder.HasOne(p => p.Receiver)
			   .WithMany(u => u.ReceivedPayments)
			   .HasForeignKey(p => p.ReceiverId)
			   .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

		builder.Property(p => p.Amount).HasPrecision(10, 0);
		builder.Property(p => p.Amount).IsRequired();
		builder.Property(p => p.Message).HasMaxLength(200);
		builder.Property(p => p.CreatedAt).HasDefaultValueSql("GETDATE()");
	}
}
