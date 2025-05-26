using Application.Features.Groups.Dto;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.Features.Groups.Command;


public record CreateListGroupCommand : IRequest<Result<List<GroupDto>>>
{
	public List<CreateGroupCommand> Groups { get; set; }
}

public class CreateListGroupCommandHandler : IRequestHandler<CreateListGroupCommand, Result<List<GroupDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public CreateListGroupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<List<GroupDto>>> Handle(CreateListGroupCommand request, CancellationToken cancellationToken)
	{
		var groups = new List<Group>();
		foreach (var item in request.Groups)
		{
			var group = new Group
			{
				Id = Guid.NewGuid(),
				Name = item.Name,
				Description = item.Description,
				CategoryId = item.CategoryId,
				CreatedBy = item.CreatedBy,
				CreatedAt = DateTime.UtcNow
			};
			groups.Add(group);
		}
		
		await _unitOfWork.Repository<Group>().AddRangeAsync(groups);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			var resultDto = _mapper.Map<List<GroupDto>>(groups);
			return Result<List<GroupDto>>.Success(resultDto, "Tạo nhóm thành công");
		}
		else
		{
			return Result<List<GroupDto>>.Failure("Không thể tạo nhóm.");
		}
	}
}
