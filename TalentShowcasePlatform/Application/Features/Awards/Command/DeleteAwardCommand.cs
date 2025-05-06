using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Awards.Command;

public record DeleteAwardCommand : IRequest<bool>
{
	public Guid Id { get; set; }
}

public class DeleteAwardCommandHandler : IRequestHandler<DeleteAwardCommand, bool>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteAwardCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<bool> Handle(DeleteAwardCommand request, CancellationToken cancellationToken)
	{
		var award = await _unitOfWork.Repository<Award>().GetByIdAsync(request.Id);

		if (award == null)
		{
			return false;
		}

		await _unitOfWork.Repository<Award>().DeleteAsync(award);
		await _unitOfWork.Save(cancellationToken);

		return true;
	}
}
