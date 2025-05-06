using Application.Features.Categories.Command;
using Application.Features.Categories.Dto;
using Application.Features.Categories.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Results;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
	private readonly IMediator _mediator;

	public CategoriesController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	public async Task<ActionResult<Result<Guid>>> CreateCategory(CreateCategoryCommand command)
	{
		return await _mediator.Send(command);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Result<CategoryDto>>> GetCategoryById(Guid id)
	{
		return await _mediator.Send(new GetCategoryByIdQuery { Id = id });
	}

	[HttpGet]
	public async Task<ActionResult<Result<IEnumerable<CategoryDto>>>> GetAllCategories()
	{
		return await _mediator.Send(new GetAllCategoriesQuery());
	}

	[HttpPut("{id}")]
	public async Task<ActionResult<Result<bool>>> UpdateCategory(Guid id, UpdateCategoryCommand command)
	{
		return await _mediator.Send(command);
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult<Result<bool>>> DeleteCategory(Guid id)
	{
		return await _mediator.Send(new DeleteCategoryCommand { Id = id });
	}
}

