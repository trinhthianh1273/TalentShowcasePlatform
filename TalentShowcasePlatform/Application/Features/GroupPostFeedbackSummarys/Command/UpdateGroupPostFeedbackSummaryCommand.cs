using Application.Features.GroupPostFeedbackSummarys.Dto;
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

namespace Application.Features.GroupPostFeedbackSummarys.Command;

public record UpdateGroupPostFeedbackSummaryCommand(Guid GroupPostId) : IRequest<Result<GroupPostFeedbackSummaryDto>>;
public class UpdateGroupPostFeedbackSummaryCommandHandler : IRequestHandler<UpdateGroupPostFeedbackSummaryCommand, Result<GroupPostFeedbackSummaryDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public UpdateGroupPostFeedbackSummaryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<GroupPostFeedbackSummaryDto>> Handle(UpdateGroupPostFeedbackSummaryCommand request, CancellationToken cancellationToken)
	{
		var ratings = await _unitOfWork.Repository<RatingGroupPost>().Entities
			.Where(r => r.GroupPostId == request.GroupPostId)
			.ToListAsync(cancellationToken);

		if (!ratings.Any())
			return Result<GroupPostFeedbackSummaryDto>.Failure("No ratings to summarize.");

		var avg = ratings.Average(r => r.Value);
		var count = ratings.Count;

		var summaryRepo = _unitOfWork.Repository<GroupPostFeedbackSummary>();

		var summary = await summaryRepo.Entities
			.FirstOrDefaultAsync(s => s.GroupPostId == request.GroupPostId, cancellationToken);

		if (summary == null)
		{
			summary = new GroupPostFeedbackSummary
			{
				GroupPostId = request.GroupPostId,
				AverageRating = avg,
				TotalRatings = count
			};
			await summaryRepo.AddAsync(summary);
		}
		else
		{
			summary.AverageRating = avg;
			summary.TotalRatings = count;
			await summaryRepo.UpdateAsync(summary);
		}

		await _unitOfWork.Save(cancellationToken);

		var dto = _mapper.Map<GroupPostFeedbackSummaryDto>(summary);
		return Result<GroupPostFeedbackSummaryDto>.Success(dto);
	}
}

