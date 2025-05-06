using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Groups.Command;

public record UpdateGroupCommand : IRequest<Result<bool>>
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
}

public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public UpdateGroupCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<bool>> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
	{
		var group = await _unitOfWork.Repository<Group>().GetByIdAsync(request.Id);

		if (group == null)
		{
			return Result<bool>.Failure("Không tìm thấy nhóm.");
		}

		group.Name = request.Name;
		group.Description = request.Description;

		await _unitOfWork.Repository<Group>().UpdateAsync(group);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<bool>.Success(true);
		}
		else
		{
			return Result<bool>.Failure("Không thể cập nhật thông tin nhóm.");
		}
	}
}


