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

public class GetAllCertificationsQuery : IRequest<Result<IEnumerable<CertificationDto>>>
{
}

public class GetAllCertificationsQueryHandler : IRequestHandler<GetAllCertificationsQuery, Result<IEnumerable<CertificationDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetAllCertificationsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<CertificationDto>>> Handle(GetAllCertificationsQuery request, CancellationToken cancellationToken)
	{
		var certifications = await _unitOfWork.Repository<Certification>()
			.GetAllAsync(include: q => q.Include(c => c.User));

		var certificationDtos = _mapper.Map<IEnumerable<CertificationDto>>(certifications);
		return Result<IEnumerable<CertificationDto>>.Success(certificationDtos);
	}
}