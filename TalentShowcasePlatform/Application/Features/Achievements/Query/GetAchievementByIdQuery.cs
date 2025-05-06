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

public record GetAchievementByIdQuery : IRequest<Result<AchievementDto>>
{
	public Guid Id { get; set; }
}

public class GetAchievementByIdQueryHandler : IRequestHandler<GetAchievementByIdQuery, Result<AchievementDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetAchievementByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<AchievementDto>> Handle(GetAchievementByIdQuery request, CancellationToken cancellationToken)
	{
		var achievement = await _unitOfWork.Repository<Achievement>()
			.GetByIdAsync(request.Id, include: q => q.Include(a => a.User));

		if (achievement == null)
		{
			return Result<AchievementDto>.Failure("Không tìm thấy Achievement.");
		}

		var achievementDto = _mapper.Map<AchievementDto>(achievement);
		return Result<AchievementDto>.Success(achievementDto);
	}
}

