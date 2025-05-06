using Application.Features.Videos.Command;
using Application.Features.Videos.Dto;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MappingProfiles;

public class VideoProfile : Profile
{
	public VideoProfile()
	{
		// Mapping từ Video sang VideoDto
		CreateMap<Video, VideoDto>()
			.ForMember(dest => dest.UserNavigationDto, opt => opt.MapFrom(src => src.User));

		CreateMap<CreateVideoCommand, Video>();
		CreateMap<UpdateVideoCommand, Video>();
	}
}
