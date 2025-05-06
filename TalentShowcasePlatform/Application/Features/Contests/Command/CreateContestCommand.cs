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

public record CreateContestCommand : IRequest<Result<Guid>>
{
	public string Title { get; set; }
	public string Description { get; set; }
	public Guid CreatedBy { get; set; }
	public DateTime StartDate { get; set; }
	public DateTime EndDate { get; set; }
}

public class CreateContestCommandHandler : IRequestHandler<CreateContestCommand, Result<Guid>>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreateContestCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<Guid>> Handle(CreateContestCommand request, CancellationToken cancellationToken)
	{
		var contest = new Contest
		{
			Id = Guid.NewGuid(),
			Title = request.Title,
			Description = request.Description,
			CreatedBy = request.CreatedBy,
			StartDate = request.StartDate,
			EndDate = request.EndDate
		};

		await _unitOfWork.Repository<Contest>().AddAsync(contest);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<Guid>.Success(contest.Id);
		}
		else
		{
			return Result<Guid>.Failure("Không thể tạo cuộc thi.");
		}
	}
}

