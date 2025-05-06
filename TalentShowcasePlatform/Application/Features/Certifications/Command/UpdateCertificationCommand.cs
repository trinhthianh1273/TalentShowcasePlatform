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

public class UpdateCertificationCommand : IRequest<Result<bool>>
{
	public Guid Id { get; set; }
	public Guid UserId { get; set; }
	public string Title { get; set; }
	public string IssuingAuthority { get; set; }
	public DateTime? IssueDate { get; set; }
	public DateTime? ExpiryDate { get; set; }
}

public class UpdateCertificationCommandHandler : IRequestHandler<UpdateCertificationCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public UpdateCertificationCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<bool>> Handle(UpdateCertificationCommand request, CancellationToken cancellationToken)
	{
		var certification = await _unitOfWork.Repository<Certification>().GetByIdAsync(request.Id);

		if (certification == null)
		{
			return Result<bool>.Failure("Không tìm thấy chứng chỉ.");
		}

		certification.UserId = request.UserId;
		certification.Title = request.Title;
		certification.IssuingAuthority = request.IssuingAuthority;
		certification.IssueDate = request.IssueDate;
		certification.ExpiryDate = request.ExpiryDate;

		await _unitOfWork.Repository<Certification>().UpdateAsync(certification);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<bool>.Success(true);
		}
		else
		{
			return Result<bool>.Failure("Không thể cập nhật thông tin chứng chỉ.");
		}
	}
}

