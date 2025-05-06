using Application.Features.Certifications.Command;
using Application.Features.Certifications.Dto;
using Application.Features.Certifications.Query;
using Shared.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CertificationsController : ControllerBase
{
	private readonly IMediator _mediator;

	public CertificationsController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	public async Task<ActionResult<Result<Guid>>> CreateCertification(CreateCertificationCommand command)
	{
		return await _mediator.Send(command);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Result<CertificationDto>>> GetCertificationById(Guid id)
	{
		return await _mediator.Send(new GetCertificationByIdQuery { Id = id });
	}

	[HttpGet]
	public async Task<ActionResult<Result<IEnumerable<CertificationDto>>>> GetAllCertifications()
	{
		return await _mediator.Send(new GetAllCertificationsQuery());
	}

	[HttpPut("{id}")]
	public async Task<ActionResult<Result<bool>>> UpdateCertification(Guid id, UpdateCertificationCommand command)
	{
		return await _mediator.Send(command);
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult<Result<bool>>> DeleteCertification(Guid id)
	{
		return await _mediator.Send(new DeleteCertificationCommand { Id = id });
	}
}
