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

public class CreatePaymentCommand : IRequest<Result<Guid>>
{
	public Guid SenderId { get; set; }
	public Guid ReceiverId { get; set; }
	public decimal Amount { get; set; }
	public string Message { get; set; }
}


public class CreatePaymentHandler : IRequestHandler<CreatePaymentCommand, Result<Guid>>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreatePaymentHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<Guid>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
	{
		var payment = new Payment
		{
			Id = Guid.NewGuid(),
			SenderId = request.SenderId,
			ReceiverId = request.ReceiverId,
			Amount = request.Amount,
			Message = request.Message,
			CreatedAt = DateTime.UtcNow
		};

		await _unitOfWork.Repository<Payment>().AddAsync(payment);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<Guid>.Success(payment.Id);
		}
		else
		{
			return Result<Guid>.Failure("Failed to create payment.");
		}
	}
}



