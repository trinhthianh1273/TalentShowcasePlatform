﻿using Application.Features.Comments.Command;
using Application.Features.Comments.Dto;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MappingProfiles;

public class CommentProfile : Profile
{
	public CommentProfile()
	{
		CreateMap<Comment, CommentDto>()
					.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));
		// You can add mappings for Video and User DTOs if you intend to include them
		// CreateMap<Video, VideoDto>();
		// CreateMap<User, UserDto>();
		CreateMap<CreateCommentCommand, Comment>();
		CreateMap<UpdateCommentCommand, Comment>();
	}
}
