using Application.Features.ContestEntries.Dto;
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

namespace Application.Features.ContestEntries.Query;

public record GetContestEntryByIdQuery : IRequest<Result<ContestEntryDto>>
{
	public Guid Id { get; set; }
}

public class GetContestEntryByIdQueryHandler : IRequestHandler<GetContestEntryByIdQuery, Result<ContestEntryDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetContestEntryByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<ContestEntryDto>> Handle(GetContestEntryByIdQuery request, CancellationToken cancellationToken)
	{
		var contestEntry = await _unitOfWork.Repository<ContestEntry>()
			.GetByIdAsync(request.Id, include: q => q.Include(ce => ce.Contest).Include(ce => ce.Video));

		if (contestEntry == null)
		{
			return Result<ContestEntryDto>.Failure("Không tìm thấy mục tham gia cuộc thi.");
		}

		return Result<ContestEntryDto>.Success(_mapper.Map<ContestEntryDto>(contestEntry));
	}
}
