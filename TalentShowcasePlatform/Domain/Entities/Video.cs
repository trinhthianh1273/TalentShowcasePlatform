using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Entities;

public class Video : BaseEntity
{
	public string Title { get; set; }
	public string Description { get; set; }
	public string Url { get; set; }
	public Guid UserId { get; set; }
	public Guid CategoryId { get; set; }
	public bool IsPrivate { get; set; }
	public DateTime UploadedAt { get; set; }

	// Navigation Properties
	public User User { get; set; }
	public Category Category { get; set; }
	public ICollection<Comment> Comments { get; set; }
	public ICollection<Rating> Ratings { get; set; }
	public ICollection<View> Views { get; set; }
	public ICollection<ContestEntry> ContestEntries { get; set; }
	public ICollection<VideoLike> VideoLikes { get; set; }
}
