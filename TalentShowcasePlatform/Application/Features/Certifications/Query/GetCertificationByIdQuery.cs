using Application.Features.Certifications.Dto;
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

namespace Application.Features.Certifications.Query;

public class GetCertificationByIdQuery : IRequest<Result<CertificationDto>>
{
	public Guid Id { get; set; }
}

public class GetCertificationByIdQueryHandler : IRequestHandler<GetCertificationByIdQuery, Result<CertificationDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetCertificationByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<CertificationDto>> Handle(GetCertificationByIdQuery request, CancellationToken cancellationToken)
	{
		var certification = await _unitOfWork.Repository<Certification>()
			.GetByIdAsync(request.Id, include: q => q.Include(c => c.User));

		if (certification == null)
		{
			return Result<CertificationDto>.Failure("Không tìm thấy chứng chỉ.");
		}

		return Result<CertificationDto>.Success(_mapper.Map<CertificationDto>(certification));
	}
}

