using Application.Features.Contests.Dto;
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

namespace Application.Features.Contests.Command;

public record CreateListContestCommand : IRequest<Result<List<ContestDto>>>
{
	public List<CreateContestCommand> Contests { get; set; }
}

public class CreateListContestCommandHandler : IRequestHandler<CreateListContestCommand, Result<List<ContestDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public CreateListContestCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<List<ContestDto>>> Handle(CreateListContestCommand request, CancellationToken cancellationToken)
	{
		var contests = new List<Contest>();
		foreach (var item in request.Contests)
		{
			var contest = new Contest
			{
				Id = Guid.NewGuid(),
				Title = item.Title,
				Description = item.Description,
				CreatedBy = item.CreatedBy,
				StartDate = item.StartDate,
				EndDate = item.EndDate
			};
			contests.Add(contest);
		}

		await _unitOfWork.Repository<Contest>().AddRangeAsync(contests);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			var contestDtos = _mapper.Map<List<ContestDto>>(contests);
			return Result<List<ContestDto>>.Success(contestDtos, "Created contest successfully.");
		}
		else
		{
			return Result<List<ContestDto>>.Failure("Không thể tạo cuộc thi.");
		}
	}
}

