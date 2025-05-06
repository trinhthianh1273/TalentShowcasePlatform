using Application.Features.Jobs.Command;
using Application.Features.Jobs.Dto;
using Application.Features.Jobs.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Results;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobsController : ControllerBase
{
	private readonly IMediator _mediator;

	public JobsController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	public async Task<ActionResult<Result<Guid>>> CreateJob(CreateJobCommand command)
	{
		return await _mediator.Send(command); 
	}

	[HttpPost]
	[Route("CreateList")]
	public async Task<ActionResult<Result<List<JobDto>>>> CreateListJob(CreateListJobCommand command)
	{
		return await _mediator.Send(command);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Result<JobDto>>> GetJobById(Guid id)
	{
		return await _mediator.Send(new GetJobByIdQuery { Id = id }); 
	}

	[HttpGet]
	public async Task<ActionResult<Result<IEnumerable<JobDto>>>> GetAllJobs()
	{
		return await _mediator.Send(new GetAllJobsQuery()); 
	}

	[HttpPut("{id}")]
	public async Task<ActionResult<Result<bool>>> UpdateJob(Guid id, UpdateJobCommand command)
	{ 
		return await _mediator.Send(command); 
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult<Result<bool>>> DeleteJob(Guid id)
	{
		return await _mediator.Send(new DeleteJobCommand { Id = id }); 
	}
}
