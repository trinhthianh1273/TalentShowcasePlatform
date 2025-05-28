using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.ValueObject;

public enum RelatedEntityType
{
	[JsonPropertyName("Group")]
	Group,

	[JsonPropertyName("GroupPost")]
	GroupPost,

	[JsonPropertyName("LikeGroupPost")]
	LikeGroupPost,

	[JsonPropertyName("CommentGroupPost")]
	CommentGroupPost,

	[JsonPropertyName("LikeCommentGroupPost")]
	LikeCommentGroupPost,

	[JsonPropertyName("RatingGroupPost")]
	RatingGroupPost,

	[JsonPropertyName("Video")]
	Video,

	[JsonPropertyName("LikeVideo")]
	LikeVideo,

	[JsonPropertyName("CommentVideo")]
	CommentVideo,
	// Thêm nếu cần
}
