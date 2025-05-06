using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Awards.Command;

public record UpdateAwardCommand : IRequest<bool>
{
	public Guid Id { get; set; }
	public Guid UserId { get; set; }
	public string Title { get; set; }
	public string AwardingOrganization { get; set; }
	public DateTime? DateReceived { get; set; }
}

public class UpdateAwardCommandHandler : IRequestHandler<UpdateAwardCommand, bool>
{
	private readonly IUnitOfWork _unitOfWork;

	public UpdateAwardCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<bool> Handle(UpdateAwardCommand request, CancellationToken cancellationToken)
	{
		var award = await _unitOfWork.Repository<Award>().GetByIdAsync(request.Id);

		if (award == null)
		{
			return false;
		}

		award.UserId = request.UserId;
		award.Title = request.Title;
		award.AwardingOrganization = request.AwardingOrganization;
		award.DateReceived = request.DateReceived;

		await _unitOfWork.Repository<Award>().UpdateAsync(award);
		await _unitOfWork.Save(cancellationToken);

		return true;
	}
}
