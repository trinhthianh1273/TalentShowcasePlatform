using Application.Features.Contests.Command;
using Application.Features.Contests.Dto;
using Application.Features.Contests.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Results;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContestsController : ControllerBase
{
	private readonly IMediator _mediator;

	public ContestsController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	public async Task<ActionResult<Result<Guid>>> CreateContest(CreateContestCommand command)
	{
		return await _mediator.Send(command);
	}

	[HttpPost]
	[Route("CreateList")]
	public async Task<ActionResult<Result<List<ContestDto>>>> CreateListContest(CreateListContestCommand command)
	{
		return await _mediator.Send(command);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Result<ContestDto>>> GetContestById(Guid id)
	{
		return await _mediator.Send(new GetContestByIdQuery { Id = id }); 
	}

	[HttpGet]
	public async Task<ActionResult<Result<IEnumerable<ContestDto>>>> GetAllContests()
	{
		return await _mediator.Send(new GetAllContestsQuery()); 
	}

	[HttpPut("{id}")]
	public async Task<ActionResult<Result<bool>>> UpdateContest(Guid id, UpdateContestCommand command)
	{ 
		return await _mediator.Send(command); 
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult<Result<bool>>> DeleteContest(Guid id)
	{
		return await _mediator.Send(new DeleteContestCommand { Id = id }); 
	}
}


