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
			.ForMember(dest => dest.UserNavigationDto, opt => opt.MapFrom(src => src.User))
			.ForMember(dest => dest.LikeCount, opt => opt.MapFrom(src => src.VideoLikes.Count))
			.ForMember(dest => dest.CommentCount, opt => opt.MapFrom(src => src.CommentVideos.Count))
			.ForMember(dest => dest.ViewCount, opt => opt.MapFrom(src => src.Views.Count));

		CreateMap<CreateVideoCommand, Video>();
		CreateMap<UpdateVideoCommand, Video>();
	}
}
