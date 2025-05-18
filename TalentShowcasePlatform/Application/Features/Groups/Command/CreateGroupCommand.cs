using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Groups.Command;


public record CreateGroupCommand : IRequest<Result<Guid>>
{
	public string Name { get; set; }
	public string Description { get; set; }
	public Guid CategoryId { get; set; }
	public Guid CreatedBy { get; set; }
}

public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, Result<Guid>>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreateGroupCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<Guid>> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
	{
		var group = new Group
		{
			Id = Guid.NewGuid(),
			Name = request.Name,
			Description = request.Description,
			CategoryId = request.CategoryId,
			CreatedBy = request.CreatedBy,
			CreatedAt = DateTime.UtcNow
		};

		await _unitOfWork.Repository<Group>().AddAsync(group);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<Guid>.Success(group.Id);
		}
		else
		{
			return Result<Guid>.Failure("Không thể tạo nhóm.");
		}
	}
}
