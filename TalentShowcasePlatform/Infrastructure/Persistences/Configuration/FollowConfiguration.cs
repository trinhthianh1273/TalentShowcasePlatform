using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistences.Configuration;

public class FollowConfiguration : IEntityTypeConfiguration<Follow>
{
	public void Configure(EntityTypeBuilder<Follow> builder)
	{
		builder.HasKey(f => new { f.FollowingUserId, f.FollowedUserId });

		builder.Property(f => f.CreatedAt)
			.IsRequired()
			.HasDefaultValueSql("GETDATE()"); // Hoặc tùy thuộc vào database của bạn

		builder.HasOne(f => f.FollowingUser)
			.WithMany(u => u.Following) // Thêm Navigation Property 'Following' vào User Entity (xem bên dưới)
			.HasForeignKey(f => f.FollowingUserId)
			.OnDelete(DeleteBehavior.Restrict); // Hoặc tùy thuộc vào logic của bạn

		builder.HasOne(f => f.FollowedUser)
			.WithMany(u => u.Followers) // Thêm Navigation Property 'Followers' vào User Entity (xem bên dưới)
			.HasForeignKey(f => f.FollowedUserId)
			.OnDelete(DeleteBehavior.Restrict); // Hoặc tùy thuộc vào logic của bạn

		builder.ToTable("Follows"); // Đảm bảo tên bảng khớp với thiết kế database
	}
}
