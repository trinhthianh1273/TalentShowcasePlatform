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

public class UpdatePaymentCommand : IRequest<Result<bool>>
{
	public Guid Id { get; set; }
	public decimal Amount { get; set; }
	public string Message { get; set; }
}

public class UpdatePaymentHandler : IRequestHandler<UpdatePaymentCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public UpdatePaymentHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<bool>> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
	{
		var payment = await _unitOfWork.Repository<Payment>().GetByIdAsync(request.Id);

		if (payment == null)
		{
			return Result<bool>.Failure("Payment not found.");
		}

		payment.Amount = request.Amount;
		payment.Message = request.Message;

		await _unitOfWork.Repository<Payment>().UpdateAsync(payment);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<bool>.Success(true);
		}
		else
		{
			return Result<bool>.Failure("Failed to update payment.");
		}
	}
}


