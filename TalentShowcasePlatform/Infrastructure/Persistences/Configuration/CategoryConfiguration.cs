﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistences.Configuration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
	public void Configure(EntityTypeBuilder<Category> builder)
	{
		builder.ToTable("Categories");
		builder.HasKey(c => c.Id);
		builder.Property(c => c.Id)
		.HasDefaultValueSql("NEWID()");
		builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
		builder.Property(c => c.Description).HasMaxLength(255);
	}
}
