using Application.Features.Messages.Command;
using Application.Features.Messages.Dto;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MappingProfiles;

public class MessageProfile : Profile
{
	public MessageProfile()
	{
		CreateMap<Message, MessageDto>();
		CreateMap<CreateMessageCommand, Message>();
		CreateMap<UpdateMessageCommand, Message>();
	}
}
