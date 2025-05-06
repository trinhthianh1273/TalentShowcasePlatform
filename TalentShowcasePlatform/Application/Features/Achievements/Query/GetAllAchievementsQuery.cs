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

namespace Application.Features.Achievements.Query;

public record GetAllAchievementsQuery : IRequest<Result<IEnumerable<AchievementDto>>>
{
}

public class GetAllAchievementsQueryHandler : IRequestHandler<GetAllAchievementsQuery, Result<IEnumerable<AchievementDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetAllAchievementsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<AchievementDto>>> Handle(GetAllAchievementsQuery request, CancellationToken cancellationToken)
	{
		var achievements = await _unitOfWork.Repository<Achievement>()
			.GetAllAsync(include: q => q.Include(a => a.User));

		var achievementDtos = _mapper.Map<IEnumerable<AchievementDto>>(achievements);
		return Result<IEnumerable<AchievementDto>>.Success(achievementDtos);
	}
}


