using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistences.Configuration;

public class WithdrawalRequestConfiguration : IEntityTypeConfiguration<WithdrawalRequest>
{
	public void Configure(EntityTypeBuilder<WithdrawalRequest> builder)
	{
		builder.HasKey(w => w.Id);

		builder.HasOne(w => w.User)
			   .WithMany(u => u.WithdrawalRequests)
			   .HasForeignKey(w => w.UserId)
			   .OnDelete(DeleteBehavior.Restrict);

		builder.Property(w => w.Amount).HasColumnType("decimal(10,0)");
		builder.Property(w => w.Status).IsRequired();
	}
}
