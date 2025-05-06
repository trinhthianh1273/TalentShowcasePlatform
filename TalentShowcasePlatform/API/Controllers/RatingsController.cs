using Application.Features.Ratings.Command;
using Application.Features.Ratings.Dto;
using Application.Features.Ratings.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Results;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RatingsController : ControllerBase
{
	private readonly IMediator _mediator;

	public RatingsController(IMediator mediator)
	{
		_mediator = mediator;
	}


	[HttpGet("{id}")]
	public async Task<ActionResult<Result<RatingDto>>> GetRatingById(Guid id)
	{
		return await _mediator.Send(new GetRatingByIdQuery { Id = id });
	}

	[HttpGet]
	public async Task<ActionResult<Result<IEnumerable<RatingDto>>>> GetAllRatings()
	{
		return await _mediator.Send(new GetAllRatingsQuery());
	}

	[HttpPost]
	public async Task<ActionResult<Result<Guid>>> CreateRating(CreateRatingCommand command)
	{
		return await _mediator.Send(command); 
	}

	[HttpPost]
	[Route("CreateList")]
	public async Task<ActionResult<Result<List<RatingDto>>>> CreateListRating(CreateRatingListCommand command)
	{
		return await _mediator.Send(command);
	}

	[HttpPut("{id}")]
	public async Task<ActionResult<Result<bool>>> UpdateRating(Guid id, UpdateRatingCommand command)
	{ 
		return await _mediator.Send(command); 
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult<Result<bool>>> DeleteRating(Guid id)
	{
		return await _mediator.Send(new DeleteRatingCommand { Id = id }); 
	}
}

