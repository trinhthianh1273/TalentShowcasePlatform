using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Payments.Command;

public class DeletePaymentCommand : IRequest<Result<bool>>
{
	public Guid Id { get; set; }
}

public class DeletePaymentHandler : IRequestHandler<DeletePaymentCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeletePaymentHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<bool>> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
	{
		var payment = await _unitOfWork.Repository<Payment>().GetByIdAsync(request.Id);

		if (payment == null)
		{
			return Result<bool>.Failure("Payment not found.");
		}

		await _unitOfWork.Repository<Payment>().DeleteAsync(payment);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<bool>.Success(true);
		}
		else
		{
			return Result<bool>.Failure("Failed to delete payment.");
		}
	}
}

