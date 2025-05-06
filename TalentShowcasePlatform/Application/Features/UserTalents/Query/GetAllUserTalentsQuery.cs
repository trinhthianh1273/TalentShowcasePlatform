using Application.Features.UserTalents.Dto;
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

namespace Application.Features.UserTalents.Query;

public class GetAllUserTalentsQuery : IRequest<Result<IEnumerable<UserTalentDto>>>
{
}

public class GetAllUserTalentsQueryHandler : IRequestHandler<GetAllUserTalentsQuery, Result<IEnumerable<UserTalentDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetAllUserTalentsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<UserTalentDto>>> Handle(GetAllUserTalentsQuery request, CancellationToken cancellationToken)
	{
		var userTalents = await _unitOfWork.Repository<UserTalent>()
			.GetAllAsync(include: q => q.Include(ut => ut.User).Include(ut => ut.Category));

		return Result<IEnumerable<UserTalentDto>>.Success(_mapper.Map<IEnumerable<UserTalentDto>>(userTalents));
	}
}


