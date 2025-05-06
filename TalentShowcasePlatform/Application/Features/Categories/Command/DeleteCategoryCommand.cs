using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Categories.Command;

public record DeleteCategoryCommand : IRequest<Result<bool>>
{
	public Guid Id { get; set; }
}

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<bool>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
	{
		var category = await _unitOfWork.Repository<Category>().GetByIdAsync(request.Id);

		if (category == null)
		{
			return Result<bool>.Failure("Không tìm thấy danh mục.");
		}

		await _unitOfWork.Repository<Category>().DeleteAsync(category);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<bool>.Success(true);
		}
		else
		{
			return Result<bool>.Failure("Không thể xóa danh mục.");
		}
	}
}

