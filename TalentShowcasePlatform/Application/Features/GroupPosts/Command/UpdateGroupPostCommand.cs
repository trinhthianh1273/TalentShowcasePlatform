using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GroupPosts.Command;

public record UpdateGroupPostCommand : IRequest<Result<bool>>
{
	public Guid Id { get; set; }
	public string Content { get; set; }
	public DateTime? LastActivityDate { get; set; }
	public string ImgUrl { get; set; }
}

public class UpdateGroupPostCommandHandler : IRequestHandler<UpdateGroupPostCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public UpdateGroupPostCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<bool>> Handle(UpdateGroupPostCommand request, CancellationToken cancellationToken)
	{
		var existingGroupPost = await _unitOfWork.Repository<GroupPost>().GetByIdAsync(request.Id);

		if (existingGroupPost == null)
		{
			return Result<bool>.Failure("Không tìm thấy bài đăng để cập nhật.");
		}

		existingGroupPost.Content = request.Content;
		existingGroupPost.LastActivityDate = request.LastActivityDate;
		existingGroupPost.ImgUrl = request.ImgUrl;

		await _unitOfWork.Repository<GroupPost>().UpdateAsync(existingGroupPost);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<bool>.Success(true, "Thành công");
		}
		else
		{
			return Result<bool>.Failure("Không thể cập nhật bài đăng.");
		}
	}
}