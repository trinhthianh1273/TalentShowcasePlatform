using Application.Features.GroupPostFeedbackSummarys.Command;
using Application.Features.GroupPostFeedbackSummarys.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GroupPostFeedbackSummaryController : ControllerBase
{
	private readonly IMediator _mediator;

	public GroupPostFeedbackSummaryController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpGet("{groupPostId}")]
	public async Task<IActionResult> Get(Guid groupPostId)
	{
		var result = await _mediator.Send(new GetGroupPostFeedbackSummaryQuery(groupPostId));
		return result.Succeeded ? Ok(result) : NotFound(result);
	}

	[HttpPost("recalculate/{groupPostId}")]
	public async Task<IActionResult> Recalculate(Guid groupPostId)
	{
		var result = await _mediator.Send(new UpdateGroupPostFeedbackSummaryCommand(groupPostId));
		return result.Succeeded ? Ok(result) : BadRequest(result);
	}
}

