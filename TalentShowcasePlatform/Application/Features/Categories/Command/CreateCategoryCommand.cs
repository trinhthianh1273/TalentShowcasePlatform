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

public record CreateCategoryCommand : IRequest<Result<Guid>>
{
	public string Name { get; set; }
	public string Description { get; set; }
}

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result<Guid>>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreateCategoryCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
	{
		var category = new Category
		{
			Id = Guid.NewGuid(),
			Name = request.Name,
			Description = request.Description
		};

		await _unitOfWork.Repository<Category>().AddAsync(category);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<Guid>.Success(category.Id);
		}
		else
		{
			return Result<Guid>.Failure("Không thể lưu thông tin danh mục.");
		}
	}
}


