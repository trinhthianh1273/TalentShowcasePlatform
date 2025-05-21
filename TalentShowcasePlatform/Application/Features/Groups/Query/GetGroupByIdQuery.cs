using Application.Features.Groups.Dto;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Groups.Query;

public record GetGroupByIdQuery : IRequest<Result<GroupDto>>
{
	public Guid Id { get; set; }
}

public class GetGroupByIdQueryHandler : IRequestHandler<GetGroupByIdQuery, Result<GroupDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetGroupByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<GroupDto>> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken)
	{
		var group = await _unitOfWork.Repository<Group>()
								.Entities
								.Where(i => i.Id == request.Id)
								.Include(i => i.CreatedByUser)
								.Include(i => i.Category)
								.FirstOrDefaultAsync(cancellationToken);

		if (group == null)
		{
			return Result<GroupDto>.Failure("Không tìm thấy nhóm.");
		}

		return Result<GroupDto>.Success(_mapper.Map<GroupDto>(group));
	}
}


