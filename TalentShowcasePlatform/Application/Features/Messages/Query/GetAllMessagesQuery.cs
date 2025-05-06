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

public record GetAllMessagesQuery : IRequest<Result<IEnumerable<MessageDto>>>
{
}

public class GetAllMessagesQueryHandler : IRequestHandler<GetAllMessagesQuery, Result<IEnumerable<MessageDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetAllMessagesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<MessageDto>>> Handle(GetAllMessagesQuery request, CancellationToken cancellationToken)
	{
		var messages = await _unitOfWork.Repository<Message>()
			.GetAllAsync(include: q => q.Include(m => m.Sender).Include(m => m.Receiver));

		return Result<IEnumerable<MessageDto>>.Success(_mapper.Map<IEnumerable<MessageDto>>(messages));
	}
}


