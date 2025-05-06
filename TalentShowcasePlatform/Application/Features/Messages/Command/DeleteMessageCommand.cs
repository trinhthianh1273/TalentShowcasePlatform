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

public record DeleteMessageCommand : IRequest<Result<bool>>
{
	public Guid Id { get; set; }
}

public class DeleteMessageHandler : IRequestHandler<DeleteMessageCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteMessageHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<bool>> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
	{
		var message = await _unitOfWork.Repository<Message>().GetByIdAsync(request.Id);

		if (message == null)
		{
			return Result<bool>.Failure("Message not found.");
		}

		await _unitOfWork.Repository<Message>().DeleteAsync(message);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<bool>.Success(true);
		}
		else
		{
			return Result<bool>.Failure("Failed to delete message.");
		}
	}
}



