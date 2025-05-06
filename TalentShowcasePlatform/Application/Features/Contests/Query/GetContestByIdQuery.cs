using Application.Features.Contests.Dto;
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

namespace Application.Features.Contests.Query;

public class GetContestByIdQuery : IRequest<Result<ContestDto>>
{
	public Guid Id { get; set; }
}

public class GetContestByIdQueryHandler : IRequestHandler<GetContestByIdQuery, Result<ContestDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetContestByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<ContestDto>> Handle(GetContestByIdQuery request, CancellationToken cancellationToken)
	{
		var contest = await _unitOfWork.Repository<Contest>()
			.GetByIdAsync(request.Id, include: q => q.Include(c => c.CreatedByUser));

		if (contest == null)
		{
			return Result<ContestDto>.Failure("Không tìm thấy cuộc thi.");
		}

		return Result<ContestDto>.Success(_mapper.Map<ContestDto>(contest));
	}
}
