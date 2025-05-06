using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Views.Command;

public class DeleteViewCommand : IRequest<Result<bool>>
{
	public Guid Id { get; set; }
}

public class DeleteViewHandler : IRequestHandler<DeleteViewCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteViewHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<bool>> Handle(DeleteViewCommand request, CancellationToken cancellationToken)
	{
		var view = await _unitOfWork.Repository<View>().GetByIdAsync(request.Id);

		if (view == null)
		{
			return Result<bool>.Failure("View not found.");
		}

		await _unitOfWork.Repository<View>().DeleteAsync(view);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<bool>.Success(true);
		}
		else
		{
			return Result<bool>.Failure("Failed to delete view.");
		}
	}
}

