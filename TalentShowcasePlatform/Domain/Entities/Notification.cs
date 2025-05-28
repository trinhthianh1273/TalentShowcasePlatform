using Domain.Common;
using Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Notification : BaseEntity
{
	public Guid UserId { get; set; } // Người nhận
	public User User { get; set; }

	public string Title { get; set; }       // Ví dụ: "Bài viết của bạn vừa được bình luận"
	public string Message { get; set; }     // Chi tiết thông báo (nếu cần)

	public string Type { get; set; }        // Loại thông báo: "Like", "Comment", "Rating", "Reply", "Mention"...

	public Guid? RelatedEntityId { get; set; } // Ví dụ: ID bài viết, comment liên quan (nullable)
	public RelatedEntityType? RelatedEntityType { get; set; }  // Nullable nếu không liên quan entity cụ thể

	public bool IsRead { get; set; } = false;
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public DateTime? ReadAt { get; set; } // Thời gian đọc thông báo (nullable, nếu chưa đọc thì null)
}
