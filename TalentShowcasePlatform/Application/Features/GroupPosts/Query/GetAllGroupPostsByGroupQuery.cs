using Application.Features.GroupPosts.Dto;
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

public record GetAllGroupPostsByGroupQuery : IRequest<Result<IEnumerable<GroupPostDto>>>
{
	public Guid GroupId { get; set; }

	public GetAllGroupPostsByGroupQuery(Guid groupId)
	{
		GroupId = groupId;
	}
}

public class GetAllGroupPostsByGroupQueryHandler : IRequestHandler<GetAllGroupPostsByGroupQuery, Result<IEnumerable<GroupPostDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetAllGroupPostsByGroupQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<GroupPostDto>>> Handle(GetAllGroupPostsByGroupQuery request, CancellationToken cancellationToken)
	{
		var groupPosts = await _unitOfWork.Repository<GroupPost>().Entities
								.Where(gp => gp.GroupId == request.GroupId)
								.Include(gp => gp.User) // Giả sử có navigation property User
								.Include(gp => gp.Group) // Giả sử có navigation property Group
								.Include(gp => gp.Comments) // Giả sử có navigation property Comments
								.Include(gp => gp.Likes) // Giả sử có navigation property Likes
							   .ToListAsync(cancellationToken);

		var dtos = _mapper.Map<IEnumerable<GroupPostDto>>(groupPosts);
		return Result<IEnumerable<GroupPostDto>>.Success(dtos);
	}
}
