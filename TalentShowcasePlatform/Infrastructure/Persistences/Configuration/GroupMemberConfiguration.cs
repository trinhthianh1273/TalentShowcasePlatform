using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistences.Configuration;

public class GroupMemberConfiguration : IEntityTypeConfiguration<GroupMember>
{
	public void Configure(EntityTypeBuilder<GroupMember> builder)
	{
		builder.ToTable("GroupMembers");
		builder.HasKey(gm => new { gm.GroupId, gm.UserId });
		builder.HasOne(gm => gm.Group)
			   .WithMany(g => g.GroupMembers)
			   .HasForeignKey(gm => gm.GroupId)
			   .OnDelete(DeleteBehavior.Restrict);
		builder.HasOne(gm => gm.User)
			   .WithMany(u => u.GroupMembers)
			   .HasForeignKey(gm => gm.UserId)
			   .OnDelete(DeleteBehavior.Restrict);
		builder.Property(gm => gm.JoinedAt).HasDefaultValueSql("GETDATE()");
	}
}
