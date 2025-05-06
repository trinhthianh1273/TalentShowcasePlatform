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

public class GetAllPaymentsQuery : IRequest<Result<IEnumerable<PaymentDto>>>
{
}

public class GetAllPaymentsQueryHandler : IRequestHandler<GetAllPaymentsQuery, Result<IEnumerable<PaymentDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetAllPaymentsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<PaymentDto>>> Handle(GetAllPaymentsQuery request, CancellationToken cancellationToken)
	{
		var payments = await _unitOfWork.Repository<Payment>()
			.GetAllAsync(include: q => q.Include(p => p.Sender).Include(p => p.Receiver));

		return Result<IEnumerable<PaymentDto>>.Success(_mapper.Map<IEnumerable<PaymentDto>>(payments));
	}
}



