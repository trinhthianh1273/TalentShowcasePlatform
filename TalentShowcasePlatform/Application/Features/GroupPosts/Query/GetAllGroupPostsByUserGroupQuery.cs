﻿using Application.Features.GroupPosts.Dto;
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

namespace Application.Features.GroupPosts.Query;

public record GetAllGroupPostsByUserGroupQuery : IRequest<Result<IEnumerable<GroupPostDto>>>
{
	public Guid UserId { get; set; }
	public Guid GroupId { get; set; }

	public GetAllGroupPostsByUserGroupQuery(Guid userId, Guid groupId)
	{
		UserId = userId;
		GroupId = groupId;
	}
}

public class GetAllGroupPostsByUserGroupQueryHandler : IRequestHandler<GetAllGroupPostsByUserGroupQuery, Result<IEnumerable<GroupPostDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
private readonly IMapper _mapper;

public GetAllGroupPostsByUserGroupQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
{
	_unitOfWork = unitOfWork;
	_mapper = mapper;
}

public async Task<Result<IEnumerable<GroupPostDto>>> Handle(GetAllGroupPostsByUserGroupQuery request, CancellationToken cancellationToken)
{
	var groupPosts = await _unitOfWork.Repository<GroupPost>().Entities
							.Where(gp => gp.GroupId == request.GroupId && gp.UserId == request.UserId)
							.Include(gp => gp.User) // Giả sử có navigation property User
							.Include(gp => gp.Group) // Giả sử có navigation property Group
						   .ToListAsync(cancellationToken);

	var dtos = _mapper.Map<IEnumerable<GroupPostDto>>(groupPosts);
	return Result<IEnumerable<GroupPostDto>>.Success(dtos);
}
}

