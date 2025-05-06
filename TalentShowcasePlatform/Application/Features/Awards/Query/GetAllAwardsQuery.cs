using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Awards.Query;

public record GetAllAwardsQuery : IRequest<IEnumerable<AwardDto>>
{
}

public class GetAllAwardsQueryHandler : IRequestHandler<GetAllAwardsQuery, IEnumerable<AwardDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetAllAwardsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<IEnumerable<AwardDto>> Handle(GetAllAwardsQuery request, CancellationToken cancellationToken)
	{
		var awards = await _unitOfWork.Repository<Award>()
			.GetAllAsync(include: q => q.Include(a => a.User));

		return _mapper.Map<IEnumerable<AwardDto>>(awards);
	}
}