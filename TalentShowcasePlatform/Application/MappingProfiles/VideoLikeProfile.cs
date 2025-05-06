using Application.Features.VideoLikes.Command;
using Application.Features.VideoLikes.Dto;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MappingProfiles;

public class VideoLikeProfile : Profile
{
	public VideoLikeProfile()
	{
		CreateMap<VideoLike, VideoLikeDto>();
		CreateMap<CreateVideoLikeCommand, VideoLike>();
	}
}
