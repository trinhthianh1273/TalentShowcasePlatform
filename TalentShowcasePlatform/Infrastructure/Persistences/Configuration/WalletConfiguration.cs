using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistences.Configuration;

public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
	public void Configure(EntityTypeBuilder<Wallet> builder)
	{
		builder.HasKey(w => w.Id);

		builder.HasOne(w => w.User)
			   .WithOne(u => u.Wallet)
			   .HasForeignKey<Wallet>(w => w.UserId)
			   .OnDelete(DeleteBehavior.Restrict);

		builder.Property(w => w.Balance).HasColumnType("decimal(10,0)");
	}
}
