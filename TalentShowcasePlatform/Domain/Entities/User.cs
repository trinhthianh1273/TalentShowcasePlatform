using Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Entities;

public class User : BaseEntity
{
	public string UserName { get; set; }
	public string Email { get; set; }
	public string PasswordHash { get; set; }
	public string? Bio { get; set; }
	public string? AvatarUrl { get; set; }
	public string? Location { get; set; }
	public Guid RoleId { get; set; }
	public DateTime CreatedAt { get; set; }

	// Navigation Properties
	public Role Role { get; set; }
	public ICollection<UserTalent> UserTalents { get; set; }
	public ICollection<Video> Videos { get; set; }
	public ICollection<Comment> Comments { get; set; }
	public ICollection<Rating> Ratings { get; set; }
	public ICollection<Group> CreatedGroups { get; set; }
	public ICollection<GroupPost> GroupPosts { get; set; }
	public ICollection<GroupMember> GroupMembers { get; set; }
	public ICollection<Contest> CreatedContests { get; set; }
	public ICollection<Message> SentMessages { get; set; }
	public ICollection<Message> ReceivedMessages { get; set; }
	public ICollection<Notification> Notifications { get; set; }
	public ICollection<View> Views { get; set; }
	public ICollection<Job> PostedJobs { get; set; }
	public ICollection<Payment> SentPayments { get; set; }
	public ICollection<Payment> ReceivedPayments { get; set; }
	public ICollection<VideoLike> VideoLikes { get; set; }
	public ICollection<Achievement> Achievements { get; set; }
	public ICollection<Award> Awards { get; set; }
	public ICollection<Certification> Certifications { get; set; }
	public virtual Wallet Wallet { get; set; }
	public ICollection<WithdrawalRequest> WithdrawalRequests { get; set; }
	public ICollection<Follow> Following { get; set; } // Danh sách những người mà người dùng này đang theo dõi
	public ICollection<Follow> Followers { get; set; } // Danh sách những người theo dõi người dùng này

}
