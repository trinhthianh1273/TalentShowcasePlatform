using Application.Features.Certifications.Command;
using Application.Features.Certifications.Dto;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MappingProfiles;

class CertificationProfiles : Profile
{
	public CertificationProfiles()
	{
		CreateMap<Certification, CertificationDto>();
		CreateMap<CreateCertificationCommand, Certification>(); // Ánh xạ từ Command về Entity khi tạo
		CreateMap<UpdateCertificationCommand, Certification>(); // Ánh xạ từ Command về Entity khi cập nhật
	}
}
