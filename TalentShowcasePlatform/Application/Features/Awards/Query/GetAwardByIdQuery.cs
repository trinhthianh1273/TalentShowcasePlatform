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

public record GetAwardByIdQuery : IRequest<AwardDto>
{
	public Guid Id { get; set; }
}

public class GetAwardByIdQueryHandler : IRequestHandler<GetAwardByIdQuery, AwardDto>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetAwardByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<AwardDto> Handle(GetAwardByIdQuery request, CancellationToken cancellationToken)
	{
		var award = await _unitOfWork.Repository<Award>()
			.GetByIdAsync(request.Id, include: q => q.Include(a => a.User));

		return _mapper.Map<AwardDto>(award);
	}
}
