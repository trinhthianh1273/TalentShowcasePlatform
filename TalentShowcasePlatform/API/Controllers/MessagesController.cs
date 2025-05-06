using Application.Features.Messages.Command;
using Application.Features.Messages.Dto;
using Application.Features.Messages.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Results;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessagesController : ControllerBase
{
	private readonly IMediator _mediator;

	public MessagesController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	public async Task<ActionResult<Result<Guid>>> CreateMessage(CreateMessageCommand messageDto)
	{
		// 1. Lấy SenderId từ claims (JWT)
		var senderIdClaim = User.FindFirst("uid");  // Hoặc ClaimTypes.NameIdentifier, tùy thuộc vào claim của bạn
		if (senderIdClaim == null || !Guid.TryParse(senderIdClaim.Value, out Guid senderId))
		{
			return BadRequest(Result<MessageDto>.Failure("Invalid sender."));
		}

		// 2. Tạo Command
		var command = new CreateMessageCommand
		{
			SenderId = senderId,
			ReceiverId = messageDto.ReceiverId,
			Content = messageDto.Content
		};

		// 3. Gửi Command
		return await _mediator.Send(command); 
	}

	[HttpPost]
	[Route("CreateList")]
	public async Task<ActionResult<Result<List<MessageDto>>>> CreateListMessage(CreateListMessageCommand messageDto)
	{
		return await _mediator.Send(messageDto);
	}



	[HttpGet("{id}")]
	public async Task<ActionResult<Result<MessageDto>>> GetMessageById(Guid id)
	{
		return await _mediator.Send(new GetMessageByIdQuery { Id = id }); 
	}

	[HttpGet]
	public async Task<ActionResult<Result<IEnumerable<MessageDto>>>> GetAllMessages()
	{
		return await _mediator.Send(new GetAllMessagesQuery()); 
	}

	[HttpPut("{id}")]
	public async Task<ActionResult<Result<bool>>> UpdateMessage(Guid id, UpdateMessageCommand command)
	{ 
		return await _mediator.Send(command); 
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult<Result<bool>>> DeleteMessage(Guid id)
	{
		return await _mediator.Send(new DeleteMessageCommand { Id = id }); 
	}
}
