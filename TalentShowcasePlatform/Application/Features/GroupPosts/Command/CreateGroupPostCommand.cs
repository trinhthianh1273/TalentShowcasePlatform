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

public record CreateGroupPostCommand : IRequest<Result<Guid>>
{
	public string Content { get; set; }
	public string ImgUrl { get; set; }
	public Guid UserId { get; set; }
	public Guid GroupId { get; set; }
}

public class CreateGroupPostCommandHandler : IRequestHandler<CreateGroupPostCommand, Result<Guid>>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreateGroupPostCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<Guid>> Handle(CreateGroupPostCommand request, CancellationToken cancellationToken)
	{
		var newGroupPost = new GroupPost
		{
			Id = Guid.NewGuid(),
			Content = request.Content,
			ImgUrl = request.ImgUrl,
			UserId = request.UserId,
			GroupId = request.GroupId,
			CreatedAt = DateTime.UtcNow,
			LastActivityDate = DateTime.UtcNow
		};

		await _unitOfWork.Repository<GroupPost>().AddAsync(newGroupPost);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<Guid>.Success(newGroupPost.Id);
		}
		else
		{
			return Result<Guid>.Failure("Không thể tạo bài đăng.");
		}
	}
}
