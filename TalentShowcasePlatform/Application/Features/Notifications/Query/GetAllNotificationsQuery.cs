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

public class GetAllNotificationsQuery : IRequest<Result<IEnumerable<NotificationDto>>>
{
}


public class GetAllNotificationsQueryHandler : IRequestHandler<GetAllNotificationsQuery, Result<IEnumerable<NotificationDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetAllNotificationsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<NotificationDto>>> Handle(GetAllNotificationsQuery request, CancellationToken cancellationToken)
	{
		var notifications = await _unitOfWork.Repository<Notification>()
			.GetAllAsync(include: q => q.Include(n => n.User));

		return Result<IEnumerable<NotificationDto>>.Success(_mapper.Map<IEnumerable<NotificationDto>>(notifications));
	}
}



