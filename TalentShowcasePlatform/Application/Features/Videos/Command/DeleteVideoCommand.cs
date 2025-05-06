using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Videos.Command;

public class DeleteVideoCommand : IRequest<Result<bool>>
{
	public Guid Id { get; set; }
}

public class DeleteVideoHandler : IRequestHandler<DeleteVideoCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteVideoHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<bool>> Handle(DeleteVideoCommand request, CancellationToken cancellationToken)
	{
		var video = await _unitOfWork.Repository<Video>().GetByIdAsync(request.Id);

		if (video == null)
		{
			return Result<bool>.Failure("Video not found.");
		}

		await _unitOfWork.Repository<Video>().DeleteAsync(video);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<bool>.Success(true);
		}
		else
		{
			return Result<bool>.Failure("Failed to delete video.");
		}
	}
}


