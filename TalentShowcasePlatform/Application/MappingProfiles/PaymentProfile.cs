using Application.Features.Payments.Command;
using Application.Features.Payments.Dto;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MappingProfiles;

public class PaymentProfile : Profile
{
	public PaymentProfile()
	{
		CreateMap<Payment, PaymentDto>();
		CreateMap<CreatePaymentCommand, Payment>();
		CreateMap<UpdatePaymentCommand, Payment>();
	}
}
