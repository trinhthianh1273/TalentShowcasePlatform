using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserTalents.Command;

public class CreateUserTalentCommand : IRequest<Result<Guid>>
{
	public Guid UserId { get; set; }
	public Guid CategoryId { get; set; }
}

public class CreateUserTalentHandler : IRequestHandler<CreateUserTalentCommand, Result<Guid>>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreateUserTalentHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<Guid>> Handle(CreateUserTalentCommand request, CancellationToken cancellationToken)
	{
		var userTalent = new UserTalent
		{
			Id = Guid.NewGuid(),
			UserId = request.UserId,
			CategoryId = request.CategoryId
		};

		await _unitOfWork.Repository<UserTalent>().AddAsync(userTalent);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<Guid>.Success(userTalent.Id);
		}
		else
		{
			return Result<Guid>.Failure("Failed to create user talent.");
		}
	}
}



