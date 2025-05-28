using Application.Features.Notifications.Dto;
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

namespace Application.Features.Notifications.Query;

public class GetNotificationByIdQuery : IRequest<Result<NotificationDto>>
{
	public Guid Id { get; set; }
}

public class GetNotificationByIdQueryHandler : IRequestHandler<GetNotificationByIdQuery, Result<NotificationDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetNotificationByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<NotificationDto>> Handle(GetNotificationByIdQuery request, CancellationToken cancellationToken)
	{
		var notification = await _unitOfWork.Repository<Notification>().GetByIdAsync(request.Id);
		if (notification == null)
			return Result<NotificationDto>.Failure("Notification not found");

		return Result<NotificationDto>.Success(_mapper.Map<NotificationDto>(notification));
	}
}

