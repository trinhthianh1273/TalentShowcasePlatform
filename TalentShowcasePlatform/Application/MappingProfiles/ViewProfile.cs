using Application.Features.Views.Command;
using Application.Features.Views.Dto;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MappingProfiles;

public class ViewProfile : Profile
{
	public ViewProfile()
	{
		CreateMap<View, ViewDto>();
		CreateMap<CreateViewCommand, View>();
	}
}
