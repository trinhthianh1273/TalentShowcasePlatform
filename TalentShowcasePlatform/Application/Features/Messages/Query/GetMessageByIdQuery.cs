using Application.Features.Messages.Dto;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Messages.Query;

public record GetMessageByIdQuery : IRequest<Result<MessageDto>>
{
	public Guid Id { get; set; }
}

public class GetMessageByIdQueryHandler : IRequestHandler<GetMessageByIdQuery, Result<MessageDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetMessageByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<MessageDto>> Handle(GetMessageByIdQuery request, CancellationToken cancellationToken)
	{
		var message = await _unitOfWork.Repository<Message>()
			.GetByIdAsync(request.Id, include: q => q.Include(m => m.Sender).Include(m => m.Receiver));

		if (message == null)
		{
			return Result<MessageDto>.Failure("Message not found.");
		}

		return Result<MessageDto>.Success(_mapper.Map<MessageDto>(message));
	}
}




