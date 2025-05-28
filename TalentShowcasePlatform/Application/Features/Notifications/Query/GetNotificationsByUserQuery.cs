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

public record GetNotificationsByUserQuery(Guid UserId) : IRequest<Result<IEnumerable<NotificationDto>>>;

public class GetNotificationsByUserQueryHandler : IRequestHandler<GetNotificationsByUserQuery, Result<IEnumerable<NotificationDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetNotificationsByUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<NotificationDto>>> Handle(GetNotificationsByUserQuery request, CancellationToken cancellationToken)
	{
		var notifications = await _unitOfWork.Repository<Notification>().Entities
								.Where(n => n.UserId == request.UserId)
								.OrderByDescending(n => n.CreatedAt)
								.ToListAsync(cancellationToken);

		return Result<IEnumerable<NotificationDto>>.Success(_mapper.Map<IEnumerable<NotificationDto>>(notifications));
	}
}