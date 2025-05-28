using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class ActivityEventPublisher : IActivityEventPublisher
{
	private readonly IUnitOfWork _unitOfWork;

	public ActivityEventPublisher(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}
	// Helper method để tạo notification
	private async Task CreateNotificationAsync(Guid userId, string title, string message, string type, Guid? relatedEntityId = null, RelatedEntityType? relatedEntityType = null)
	{
		var notification = new Notification
		{
			UserId = userId,
			Title = title,
			Message = message,
			Type = type,
			RelatedEntityId = relatedEntityId,
			RelatedEntityType = relatedEntityType,
			CreatedAt = DateTime.UtcNow,
			IsRead = false
		};

		await _unitOfWork.Repository<Notification>().AddAsync(notification);
		await _unitOfWork.Save();
	}

	public async Task PublishLikeVideoAsync(Guid actorUserId, Guid targetUserId, Guid videoId)
	{
		if (actorUserId == targetUserId) return; // Không tự gửi notification cho chính mình
		var userName = await GetUserNameAsync(actorUserId);

		var title = "Video của bạn vừa được thích";
		var message = $"{userName} đã thích video của bạn.";
		await CreateNotificationAsync(targetUserId, title, message, "LikeVideo", videoId, RelatedEntityType.LikeVideo);
	}

	public async Task PublishCommentVideoAsync(Guid actorUserId, Guid targetUserId, Guid videoId, Guid commentId)
	{
		if (actorUserId == targetUserId) return;
		var userName = await GetUserNameAsync(actorUserId);

		var title = "Video của bạn vừa được bình luận";
		var message = $"{userName} đã bình luận trên video của bạn.";
		await CreateNotificationAsync(targetUserId, title, message, "CommentVideo", commentId, RelatedEntityType.CommentVideo);
	}

	public async Task PublishJoinGroupAsync(Guid actorUserId, Guid targetUserId, Guid groupId)
	{
		if (actorUserId == targetUserId) return; // Không tự gửi notification cho chính mình
		var userName = await GetUserNameAsync(actorUserId);
		var groupName = await GetGroupNameAsync(groupId);

		var title = "Thành viên mới tham gia nhóm";
		var message = $"{userName} đã tham gia nhóm: {groupName}.";
		await CreateNotificationAsync(targetUserId, title, message, "JoinGroup", groupId, RelatedEntityType.Group);
	}

	public async Task PublishOutGroupAsync(Guid actorUserId, Guid targetUserId, Guid groupId)
	{
		if (actorUserId == targetUserId) return; // Không tự gửi notification cho chính mình
		var userName = await GetUserNameAsync(actorUserId);
		var groupName = await GetGroupNameAsync(groupId);

		var title = "Có thành viên rời nhóm";
		var message = $"{userName} đã rời nhóm : {groupName}.";
		await CreateNotificationAsync(targetUserId, title, message, "OutGroup", groupId, RelatedEntityType.Group);
	}

	public async Task PublishCreatePostInGroupAsync(Guid actorUserId, Guid targetUserId, Guid groupId, Guid postId)
	{
		var userName = await GetUserNameAsync(actorUserId);
		var groupName = await GetGroupNameAsync(groupId);

		var title = "Bài viết mới trong nhóm của bạn";
		var message = $"{userName} đã tạo bài viết mới trong nhóm : {groupName}.";
		await CreateNotificationAsync(targetUserId, title, message, "CreatePostInGroup", postId, RelatedEntityType.GroupPost);
	}

	public async Task PublishLikeGroupPostAsync(Guid actorUserId, Guid targetUserId, Guid groupPostId)
	{
		if (actorUserId == targetUserId) return;
		var userName = await GetUserNameAsync(actorUserId);

		var title = "Bài viết nhóm của bạn được thích";
		var message = $"{userName} đã thích bài viết nhóm của bạn.";
		await CreateNotificationAsync(targetUserId, title, message, "LikeGroupPost", groupPostId, RelatedEntityType.LikeGroupPost);
	}

	public async Task PublishCommentGroupPostAsync(Guid actorUserId, Guid targetUserId, Guid groupPostId, Guid commentId)
	{
		if (actorUserId == targetUserId) return;
		var userName = await GetUserNameAsync(actorUserId);

		var title = "Bài viết nhóm của bạn vừa được bình luận";
		var message = $"{userName} đã bình luận trên bài viết nhóm của bạn.";
		await CreateNotificationAsync(targetUserId, title, message, "CommentGroupPost", commentId, RelatedEntityType.CommentGroupPost);
	}
	public async Task PublishLikeCommentGroupPostAsync(Guid actorUserId, Guid targetUserId, Guid groupPostId, Guid likeCommentId)
	{
		if (actorUserId == targetUserId) return;
		var userName = await GetUserNameAsync(actorUserId);

		var title = "Bình luận của bạn vừa được thích";
		var message = $"{userName} đã thích bình luận của bạn.";
		await CreateNotificationAsync(targetUserId, title, message, "LikeCommentGroupPost", likeCommentId, RelatedEntityType.LikeCommentGroupPost);
	}

	public async Task PublishReplyCommentAsync(Guid actorUserId, Guid targetUserId, Guid groupPostId, Guid commentId, Guid parentCommentId)
	{
		if (actorUserId == targetUserId) return;
		var userName = await GetUserNameAsync(actorUserId);

		var title = "Bình luận của bạn vừa được trả lời";
		var message = $"{userName} đã trả lời bình luận của bạn.";
		await CreateNotificationAsync(targetUserId, title, message, "ReplyComment", commentId, RelatedEntityType.CommentGroupPost);
	}

	public async Task<string> GetUserNameAsync(Guid actorUserId)
	{
		var user = await _unitOfWork.Repository<User>().GetByIdAsync(actorUserId);
		var userName = user?.UserName ?? "Người dùng";

		return userName;
	}

	public async Task<string> GetGroupNameAsync(Guid groupId)
	{
		var group = await _unitOfWork.Repository<Group>().GetByIdAsync(groupId);
		var groupName = group?.Name ?? "Nhóm";

		return groupName;
	}
}
