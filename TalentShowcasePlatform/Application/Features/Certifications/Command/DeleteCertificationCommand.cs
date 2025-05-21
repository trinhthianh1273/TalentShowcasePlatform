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

public class DeleteCertificationCommand : IRequest<Result<bool>>
{
	public Guid Id { get; set; }
}

public class DeleteCertificationCommandHandler : IRequestHandler<DeleteCertificationCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteCertificationCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<bool>> Handle(DeleteCertificationCommand request, CancellationToken cancellationToken)
	{
		var certification = await _unitOfWork.Repository<Certification>().GetByIdAsync(request.Id);

		if (certification == null)
		{
			return Result<bool>.Failure("Không tìm thấy chứng chỉ.");
		}

		await _unitOfWork.Repository<Certification>().DeleteAsync(certification);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<bool>.Success(true);
		}
		else
		{
			return Result<bool>.Failure("Không thể xóa chứng chỉ.");
		}
	}
}

