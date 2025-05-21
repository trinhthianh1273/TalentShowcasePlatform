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

namespace Application.Features.GroupPostFeedbackSummarys.Query;

public record GetGroupPostFeedbackSummaryQuery(Guid GroupPostId) : IRequest<Result<GroupPostFeedbackSummaryDto>>;

public class GetGroupPostFeedbackSummaryQueryHandler : IRequestHandler<GetGroupPostFeedbackSummaryQuery, Result<GroupPostFeedbackSummaryDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetGroupPostFeedbackSummaryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<GroupPostFeedbackSummaryDto>> Handle(GetGroupPostFeedbackSummaryQuery request, CancellationToken cancellationToken)
	{
		var summary = await _unitOfWork.Repository<GroupPostFeedbackSummary>().Entities
			.FirstOrDefaultAsync(x => x.GroupPostId == request.GroupPostId, cancellationToken);

		if (summary == null)
			return Result<GroupPostFeedbackSummaryDto>.Failure("No summary found.");

		var dto = _mapper.Map<GroupPostFeedbackSummaryDto>(summary);
		return Result<GroupPostFeedbackSummaryDto>.Success(dto);
	}
}

