using Application.Features.GroupPostFeedbackSummarys.Dto;
using Application.Features.GroupPosts.Dto;
using Application.Features.LikeCommentGroupPosts.Dto;
using Application.Features.LikeGroupPosts.Dto;
using Application.Features.RatingGroupPosts.Dto;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MappingProfiles;

public class GroupPostMappingProfile : Profile
{
	public GroupPostMappingProfile()
	{
		CreateMap<GroupPost, GroupPostDto>()
			.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
			.ForMember(dest => dest.UserImgUrl, opt => opt.MapFrom(src => src.User.AvatarUrl))
			.ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.Group.Name));
		// Ánh xạ các thuộc tính còn lại một cách tự động nếu tên trùng khớp

		// RatingGroupPost → RatingGroupPostDto
		CreateMap<RatingGroupPost, RatingGroupPostDto>()
			.ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
			.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
			.ForMember(dest => dest.UserAvatar, opt => opt.MapFrom(src => src.User.AvatarUrl));

		// LikeGroupPost → LikeGroupPostDto
		CreateMap<LikeGroupPost, LikeGroupPostDto>()
			.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
			.ForMember(dest => dest.UserAvatar, opt => opt.MapFrom(src => src.User.AvatarUrl));

		// LikeCommentGroupPost → LikeCommentGroupPostDto
		CreateMap<LikeCommentGroupPost, LikeCommentGroupPostDto>()
			.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
			.ForMember(dest => dest.UserAvatar, opt => opt.MapFrom(src => src.User.AvatarUrl));

		// GroupPostFeedbackSummary → GroupPostFeedbackSummaryDto
		CreateMap<GroupPostFeedbackSummary, GroupPostFeedbackSummaryDto>();
	}
}
