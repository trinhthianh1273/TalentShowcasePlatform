using Application.Features.Categories.Dto;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Categories.Query;

public record GetAllCategoriesQuery : IRequest<Result<IEnumerable<CategoryDto>>>
{
}

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, Result<IEnumerable<CategoryDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetAllCategoriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<CategoryDto>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
	{
		var categories = await _unitOfWork.Repository<Category>().GetAllAsync();
		return Result<IEnumerable<CategoryDto>>.Success(_mapper.Map<IEnumerable<CategoryDto>>(categories));
	}
}

