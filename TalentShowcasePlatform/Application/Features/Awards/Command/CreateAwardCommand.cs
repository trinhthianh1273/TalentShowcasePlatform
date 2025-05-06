using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Awards.Command;

public record CreateAwardCommand : IRequest<Guid>
{
	public Guid UserId { get; set; }
	public string Title { get; set; }
	public string AwardingOrganization { get; set; }
	public DateTime? DateReceived { get; set; }
}

public class CreateAwardCommandHandler : IRequestHandler<CreateAwardCommand, Guid>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreateAwardCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Guid> Handle(CreateAwardCommand request, CancellationToken cancellationToken)
	{
		var award = new Award
		{
			Id = Guid.NewGuid(),
			UserId = request.UserId,
			Title = request.Title,
			AwardingOrganization = request.AwardingOrganization,
			DateReceived = request.DateReceived,
			CreatedAt = DateTime.UtcNow
		};

		await _unitOfWork.Repository<Award>().AddAsync(award);
		await _unitOfWork.Save(cancellationToken);

		return award.Id;
	}
}