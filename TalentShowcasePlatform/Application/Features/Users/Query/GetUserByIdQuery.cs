using Application.Features.Users.Dto;
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

namespace Application.Features.Users.Query;

public class GetUserByIdQuery : IRequest<Result<UserDto>>
{
	public Guid Id { get; set; }
}

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetUserByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
	{
		var user = await _unitOfWork.Repository<User>().Entities
						.Where(i => i.Id == request.Id)
						.Include(i => i.Role)
						.FirstOrDefaultAsync(cancellationToken);

		if (user == null)
		{
			return Result<UserDto>.Failure("User not found.");
		}

		return Result<UserDto>.Success(_mapper.Map<UserDto>(user));
	}
}


