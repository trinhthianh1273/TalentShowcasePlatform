using Application.Features.Payments.Dto;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Payments.Query;

public class GetPaymentByIdQuery : IRequest<Result<PaymentDto>>
{
	public Guid Id { get; set; }
}

public class GetPaymentByIdQueryHandler : IRequestHandler<GetPaymentByIdQuery, Result<PaymentDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetPaymentByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<PaymentDto>> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
	{
		var payment = await _unitOfWork.Repository<Payment>()
			.GetByIdAsync(request.Id, include: q => q.Include(p => p.Sender).Include(p => p.Receiver));

		if (payment == null)
		{
			return Result<PaymentDto>.Failure("Payment not found.");
		}

		return Result<PaymentDto>.Success(_mapper.Map<PaymentDto>(payment));
	}
}



