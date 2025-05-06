using Application.Features.Achievements.Command;
using Application.Features.Achievements.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Results;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AchievementsController : ControllerBase
{
	private readonly IMediator _mediator;

	public AchievementsController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	public async Task<ActionResult<Result<Guid>>> CreateAchievement(CreateAchievementCommand command)
	{
		return await _mediator.Send(command);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Result<AchievementDto>>> GetAchievementById(Guid id)
	{
		return await _mediator.Send(new GetAchievementByIdQuery { Id = id });
	}

	[HttpGet]
	public async Task<ActionResult<Result<IEnumerable<AchievementDto>>>> GetAllAchievements()
	{
		return await _mediator.Send(new GetAllAchievementsQuery());
	}

	[HttpPut("{id}")]
	public async Task<ActionResult<Result<bool>>> UpdateAchievement(Guid id, UpdateAchievementCommand command)
	{
		return await _mediator.Send(command);
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult<Result<bool>>> DeleteAchievement(Guid id)
	{
		return await _mediator.Send(new DeleteAchievementCommand { Id = id });
	}
}