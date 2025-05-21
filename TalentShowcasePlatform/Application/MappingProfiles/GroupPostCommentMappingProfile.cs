using Application.Features.GroupPostComments.Command;
using Application.Features.GroupPostComments.Dto;
using Application.Features.Groups.Command;
using Application.Features.Groups.Dto;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MappingProfiles;

public class GroupPostCommentMappingProfile : Profile
{
	public GroupPostCommentMappingProfile()
	{
		CreateMap<CommentGroupPost, GroupPostCommentDto>()
			.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
			.ForMember(dest => dest.UserImgUrl, opt => opt.MapFrom(src => src.User.AvatarUrl));
		CreateMap<CreateGroupPostCommentCommand, CommentGroupPost>();
		CreateMap<UpdateGroupPostCommentCommand, CommentGroupPost>();
	}
}
