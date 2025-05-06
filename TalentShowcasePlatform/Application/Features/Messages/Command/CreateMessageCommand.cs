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

public record CreateMessageCommand : IRequest<Result<Guid>>
{
	public Guid SenderId { get; set; }
	public Guid ReceiverId { get; set; }
	public string Content { get; set; }
	public DateTime SentAt { get; set; }
}

public class CreateMessageHandler : IRequestHandler<CreateMessageCommand, Result<Guid>>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreateMessageHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<Guid>> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
	{
		var message = new Message
		{
			Id = Guid.NewGuid(),
			SenderId = request.SenderId,
			ReceiverId = request.ReceiverId,
			Content = request.Content
		};

		await _unitOfWork.Repository<Message>().AddAsync(message);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<Guid>.Success(message.Id);
		}
		else
		{
			return Result<Guid>.Failure("Failed to create message.");
		}
	}
}




