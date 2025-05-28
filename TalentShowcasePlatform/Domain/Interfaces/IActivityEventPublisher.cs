using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces;

public interface IActivityEventPublisher
{
	Task PublishLikeVideoAsync(Guid actorUserId, Guid targetUserId, Guid videoId);
	Task PublishCommentVideoAsync(Guid actorUserId, Guid targetUserId, Guid videoId, Guid commentId);

	Task PublishJoinGroupAsync(Guid actorUserId, Guid targetUserId, Guid groupId);
	Task PublishOutGroupAsync(Guid actorUserId, Guid targetUserId, Guid groupId);

	Task PublishCreatePostInGroupAsync(Guid actorUserId, Guid targetUserId, Guid groupId, Guid postId);
	Task PublishLikeGroupPostAsync(Guid actorUserId, Guid targetUserId, Guid groupPostId);
	Task PublishLikeCommentGroupPostAsync(Guid actorUserId, Guid targetUserId, Guid groupPostId, Guid likeCommentId);
	Task PublishCommentGroupPostAsync(Guid actorUserId, Guid targetUserId, Guid groupPostId, Guid commentId);
	Task PublishReplyCommentAsync(Guid actorUserId, Guid targetUserId, Guid groupPostId, Guid commentId, Guid parentCommentId);

	Task<string> GetUserNameAsync(Guid actorUserId);
	Task<string> GetGroupNameAsync(Guid groupId);
}


