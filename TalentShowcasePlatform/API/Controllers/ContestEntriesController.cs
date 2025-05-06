using Application.Features.ContestEntries.Command;
using Application.Features.ContestEntries.Dto;
using Application.Features.ContestEntries.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Results;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContestEntriesController : ControllerBase
{
	private readonly IMediator _mediator;

	public ContestEntriesController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	public async Task<ActionResult<Result<Guid>>> CreateContestEntry(CreateContestEntryCommand command)
	{
		return await _mediator.Send(command);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Result<ContestEntryDto>>> GetContestEntryById(Guid id)
	{
		return await _mediator.Send(new GetContestEntryByIdQuery { Id = id });
	}

	[HttpGet]
	public async Task<ActionResult<Result<IEnumerable<ContestEntryDto>>>> GetAllContestEntries()
	{
		return await _mediator.Send(new GetAllContestEntriesQuery());
	}

	[HttpGet("contest/{contestId}")]
	public async Task<ActionResult<Result<IEnumerable<ContestEntryDto>>>> GetContestEntriesByContestId(Guid contestId)
	{
		return await _mediator.Send(new GetContestEntriesByContestIdQuery { ContestId = contestId });
	}

	[HttpGet("video/{videoId}")]
	public async Task<ActionResult<Result<IEnumerable<ContestEntryDto>>>> GetContestEntriesByVideoId(Guid videoId)
	{
		return await _mediator.Send(new GetContestEntriesByVideoIdQuery { VideoId = videoId });
	}

	[HttpPut("{id}")]
	public async Task<ActionResult<Result<bool>>> UpdateContestEntry(Guid id, UpdateContestEntryCommand command)
	{
		return await _mediator.Send(command);
	}

	[HttpPut("{id}/vote")]
	public async Task<ActionResult<Result<bool>>> AddVote(Guid id)
	{
		return await _mediator.Send(new AddVoteCommand { Id = id });
	}
    

        [HttpDelete("{id}/vote")]
	public async Task<ActionResult<Result<bool>>> RemoveVote(Guid id)
	{
		return await _mediator.Send(new RemoveVoteCommand { Id = id });
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult<Result<bool>>> DeleteContestEntry(Guid id)
	{
		return await _mediator.Send(new DeleteContestEntryCommand { Id = id });
	}
}
