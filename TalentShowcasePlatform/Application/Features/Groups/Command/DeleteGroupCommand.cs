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

public record DeleteGroupCommand : IRequest<Result<bool>>
{
	public Guid Id { get; set; }
}

public class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteGroupCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<bool>> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
	{
		var group = await _unitOfWork.Repository<Group>().GetByIdAsync(request.Id);

		if (group == null)
		{
			return Result<bool>.Failure("Không tìm thấy nhóm.");
		}

		await _unitOfWork.Repository<Group>().DeleteAsync(group);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<bool>.Success(true);
		}
		else
		{
			return Result<bool>.Failure("Không thể xóa nhóm.");
		}
	}
}
