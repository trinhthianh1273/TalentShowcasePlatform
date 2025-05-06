using Application.Features.Messages.Dto;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Messages.Command;

public record CreateListMessageCommand : IRequest<Result<List<MessageDto>>>
{
	public List<CreateMessageCommand> Messages { get; set; }
}

public class CreateListMessageCommandHandler : IRequestHandler<CreateListMessageCommand, Result<List<MessageDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public CreateListMessageCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<List<MessageDto>>> Handle(CreateListMessageCommand request, CancellationToken cancellationToken)
	{
		var Messages = new List<Message>();
		foreach (var item in request.Messages)
		{
			var message = new Message
			{
				Id = Guid.NewGuid(),
				SenderId = item.SenderId,
				ReceiverId = item.ReceiverId,
				Content = item.Content,
				SentAt = item.SentAt
			};
			Messages.Add(message);
		}
		

		await _unitOfWork.Repository<Message>().AddRangeAsync(Messages);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			var resultDto = _mapper.Map<List<MessageDto>>(Messages);
			return Result<List<MessageDto>>.Success(resultDto, "Đã gửi tin nhắn");
		}
		else
		{
			return Result<List<MessageDto>>.Failure("Failed to send message.");
		}
	}
}




