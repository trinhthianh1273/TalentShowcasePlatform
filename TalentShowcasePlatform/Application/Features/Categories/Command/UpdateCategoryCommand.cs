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

public record UpdateCategoryCommand : IRequest<Result<bool>>
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
}

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<bool>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
	{
		var category = await _unitOfWork.Repository<Category>().GetByIdAsync(request.Id);

		if (category == null)
		{
			return Result<bool>.Failure("Không tìm thấy danh mục.");
		}

		category.Name = request.Name;
		category.Description = request.Description;

		await _unitOfWork.Repository<Category>().UpdateAsync(category);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<bool>.Success(true);
		}
		else
		{
			return Result<bool>.Failure("Không thể cập nhật thông tin danh mục.");
		}
	}
}

