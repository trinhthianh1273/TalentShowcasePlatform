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

public record UpdateMessageCommand : IRequest<Result<bool>>
{
	public Guid Id { get; set; }
	public string Content { get; set; }
}

public class UpdateMessageHandler : IRequestHandler<UpdateMessageCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public UpdateMessageHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<bool>> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
	{
		var message = await _unitOfWork.Repository<Message>().GetByIdAsync(request.Id);

		if (message == null)
		{
			return Result<bool>.Failure("Message not found.");
		}

		message.Content = request.Content;

		await _unitOfWork.Repository<Message>().UpdateAsync(message);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<bool>.Success(true);
		}
		else
		{
			return Result<bool>.Failure("Failed to update message.");
		}
	}
}



