using Application.Features.Notifications.Command;
using Application.Features.Notifications.Dto;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MappingProfiles;

public class NotificationProfile : Profile
{
	public NotificationProfile()
	{
		CreateMap<Notification, NotificationDto>();
		CreateMap<CreateNotificationCommand, Notification>();
		CreateMap<UpdateNotificationCommand, Notification>();
	}
}
