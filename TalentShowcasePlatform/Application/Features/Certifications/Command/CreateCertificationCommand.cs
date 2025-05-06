using Application.Features.Achievements.Command;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Certifications.Command;

public class CreateCertificationCommand : IRequest<Result<Guid>>
{
	public Guid UserId { get; set; }
	public string Title { get; set; }
	public string IssuingAuthority { get; set; }
	public DateTime? IssueDate { get; set; }
	public DateTime? ExpiryDate { get; set; }
}

public class CreateCertificationCommandHandler : IRequestHandler<CreateCertificationCommand, Result<Guid>>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreateCertificationCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<Guid>> Handle(CreateCertificationCommand request, CancellationToken cancellationToken)
	{
		var certification = new Certification
		{
			Id = Guid.NewGuid(),
			UserId = request.UserId,
			Title = request.Title,
			IssuingAuthority = request.IssuingAuthority,
			IssueDate = request.IssueDate,
			ExpiryDate = request.ExpiryDate,
			CreatedAt = DateTime.UtcNow
		};

		await _unitOfWork.Repository<Certification>().AddAsync(certification);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<Guid>.Success(certification.Id);
		}
		else
		{
			return Result<Guid>.Failure("Không thể lưu thông tin chứng chỉ.");
		}
	}
}

