using Application.Features.Ratings.Command;
using Application.Features.Ratings.Dto;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MappingProfiles;

public class RatingProfile : Profile
{
	public RatingProfile()
	{
		CreateMap<Rating, RatingDto>();
		CreateMap<CreateRatingCommand, Rating>();
		CreateMap<UpdateRatingCommand, Rating>();
	}
}
