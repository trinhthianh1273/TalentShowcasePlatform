using Application.Features.Messages.Command;
using Application.Features.Messages.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace API.Hubs;

[Authorize] // Yêu cầu người dùng đã xác thực
public class ChatHub : Hub
{
	private readonly IMediator _mediator;

	public ChatHub(IMediator mediator)
	{
		_mediator = mediator;
	}

	public async Task SendMessage(SendMessageDto messageDto)
	{
		try
		{
			// 1. Lấy SenderId từ claims (JWT)
			var senderIdClaim = Context.User.FindFirst(ClaimTypes.NameIdentifier);
			if (senderIdClaim == null || !Guid.TryParse(senderIdClaim.Value, out Guid senderId))
			{
				await Clients.Caller.SendAsync("MessageFailed", "Invalid sender.");
				return;
			}

			// 2. Tạo Command để lưu tin nhắn
			var command = new CreateMessageCommand
			{
				SenderId = senderId,
				ReceiverId = messageDto.ReceiverId,
				Content = messageDto.Content
			};

			// 3. Gửi Command đến Handler
			var result = await _mediator.Send(command);

			if (result.Succeeded)
			{
				// 4. Gửi tin nhắn đến người nhận
				await Clients.User(messageDto.ReceiverId.ToString())
							   .SendAsync("ReceiveMessage", result.Data); // result.Data là MessageDto

				// 5. Gửi tin nhắn đến người gửi (để hiển thị ngay lập tức)
				await Clients.Caller.SendAsync("ReceiveMessage", result.Data);
			}
			else
			{
				// 6. Xử lý lỗi
				await Clients.Caller.SendAsync("MessageFailed", result.Messages);
			}
		}
		catch (Exception ex)
		{
			// 7. Log lỗi (quan trọng!)
			Console.Error.WriteLine(ex);
			await Clients.Caller.SendAsync("MessageFailed", "An error occurred while sending the message.");
		}
	}

	public override async Task OnConnectedAsync()
	{
		// 8. Xử lý khi client kết nối (ví dụ: thông báo)
		Console.WriteLine($"User Connected: {Context.ConnectionId}");
		await base.OnConnectedAsync();
	}

	public override async Task OnDisconnectedAsync(Exception exception)
	{
		// 9. Xử lý khi client ngắt kết nối
		if (exception != null)
		{
			Console.Error.WriteLine($"Connection Disconnected with Error: {exception}");
		}
		else
		{
			Console.WriteLine($"User Disconnected: {Context.ConnectionId}");
		}
		await base.OnDisconnectedAsync(exception);
	}
}
