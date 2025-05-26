using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistences.Configuration;

public class JobConfiguration : IEntityTypeConfiguration<Job>
{
	public void Configure(EntityTypeBuilder<Job> builder)
	{
		builder.ToTable("Jobs");
		builder.HasKey(j => j.Id);
		builder.Property(j => j.Id)
		.HasDefaultValueSql("NEWID()");

		builder.Property(j => j.Title)
				.IsRequired()
				.HasMaxLength(255);

		builder.Property(j => j.CompanyName)
			.HasMaxLength(255); // Không bắt buộc, nên không có IsRequired()

		builder.Property(j => j.Location)
			.IsRequired()
			.HasMaxLength(255);

		builder.Property(j => j.AddressDetail)
			.HasMaxLength(255);

		builder.Property(j => j.Description)
			.HasColumnType("NVARCHAR(MAX)"); // Sử dụng TEXT cho phép lưu trữ văn bản dài

		builder.Property(j => j.Requirements)
			.HasColumnType("NVARCHAR(MAX)");

		builder.Property(j => j.Benefits)
			.HasColumnType("NVARCHAR(MAX)");

		builder.Property(j => j.JobType)
			.HasMaxLength(50); // Chọn độ dài phù hợp

		builder.Property(j => j.SalaryFrom)
			.HasColumnType("DECIMAL(18, 2)"); // Hoặc kiểu dữ liệu phù hợp với database của bạn

		builder.Property(j => j.SalaryTo)
			.HasColumnType("DECIMAL(18, 2)"); // Hoặc kiểu dữ liệu phù hợp

		builder.Property(j => j.ExpiryDate); // Kiểu DateTime mặc định đã ổn

		builder.Property(j => j.ContactEmail)
			.HasMaxLength(255);

		builder.Property(j => j.ContactPhone)
			.HasMaxLength(20);

		builder.Property(j => j.PostedBy)
			.IsRequired();

		builder.Property(j => j.CategoryId)
			.IsRequired();

		builder.Property(j => j.CreatedAt)
			.IsRequired()
			.HasDefaultValueSql("GETUTCDATE()"); // Giá trị mặc định cho SQL Server


		builder.HasOne(j => j.PostedByUser)
			   .WithMany(u => u.PostedJobs)
			   .HasForeignKey(j => j.PostedBy)
			   .OnDelete(DeleteBehavior.Restrict);
		builder.Property(j => j.CreatedAt).HasDefaultValueSql("GETDATE()");
	}
}
