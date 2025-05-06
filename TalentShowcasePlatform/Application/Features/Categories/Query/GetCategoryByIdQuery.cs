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

public record GetCategoryByIdQuery : IRequest<Result<CategoryDto>>
{
	public Guid Id { get; set; }
}

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Result<CategoryDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
	{
		var category = await _unitOfWork.Repository<Category>().GetByIdAsync(request.Id);

		if (category == null)
		{
			return Result<CategoryDto>.Failure("Không tìm thấy danh mục.");
		}

		return Result<CategoryDto>.Success(_mapper.Map<CategoryDto>(category));
	}
}

