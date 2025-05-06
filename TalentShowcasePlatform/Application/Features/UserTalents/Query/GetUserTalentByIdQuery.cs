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

public class GetUserTalentByIdQuery : IRequest<Result<UserTalentDto>>
{
	public Guid Id { get; set; }
}

public class GetUserTalentByIdQueryHandler : IRequestHandler<GetUserTalentByIdQuery, Result<UserTalentDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetUserTalentByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<UserTalentDto>> Handle(GetUserTalentByIdQuery request, CancellationToken cancellationToken)
	{
		var userTalent = await _unitOfWork.Repository<UserTalent>()
			.GetByIdAsync(request.Id, include: q => q.Include(ut => ut.User).Include(ut => ut.Category));

		if (userTalent == null)
		{
			return Result<UserTalentDto>.Failure("User talent not found.");
		}

		return Result<UserTalentDto>.Success(_mapper.Map<UserTalentDto>(userTalent));
	}
}



