using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Views.Command;

public class CreateViewCommand : IRequest<Result<Guid>>
{
	public Guid VideoId { get; set; }
	public Guid ViewerId { get; set; }
}

public class CreateViewHandler : IRequestHandler<CreateViewCommand, Result<Guid>>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreateViewHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<Guid>> Handle(CreateViewCommand request, CancellationToken cancellationToken)
	{
		var view = new View
		{
			Id = Guid.NewGuid(),
			VideoId = request.VideoId,
			ViewerId = request.ViewerId
		};

		await _unitOfWork.Repository<View>().AddAsync(view);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<Guid>.Success(view.Id);
		}
		else
		{
			return Result<Guid>.Failure("Failed to create view.");
		}
	}
}



