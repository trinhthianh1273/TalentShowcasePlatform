USE [TalentShowcasePlatformDB]
GO
/****** Object:  Table [dbo].[Awards]    Script Date: 5/27/2025 6:32:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Awards](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[AwardingOrganization] [nvarchar](255) NOT NULL,
	[DateReceived] [datetime2](7) NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Awards] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 5/27/2025 6:32:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Certifications]    Script Date: 5/27/2025 6:32:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Certifications](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[IssuingAuthority] [nvarchar](255) NOT NULL,
	[IssueDate] [datetime2](7) NULL,
	[ExpiryDate] [datetime2](7) NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Certifications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CommentGroupPosts]    Script Date: 5/27/2025 6:32:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CommentGroupPosts](
	[Id] [uniqueidentifier] NOT NULL,
	[GroupPostId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[ParentCommentId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_CommentGroupPosts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CommentVideos]    Script Date: 5/27/2025 6:32:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CommentVideos](
	[Id] [uniqueidentifier] NOT NULL,
	[VideoId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Content] [nvarchar](1000) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_CommentVideos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContestEntries]    Script Date: 5/27/2025 6:32:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContestEntries](
	[Id] [uniqueidentifier] NOT NULL,
	[ContestId] [uniqueidentifier] NOT NULL,
	[VideoId] [uniqueidentifier] NOT NULL,
	[SubmittedAt] [datetime2](7) NOT NULL,
	[Votes] [int] NOT NULL,
 CONSTRAINT [PK_ContestEntries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contests]    Script Date: 5/27/2025 6:32:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contests](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](1000) NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[EndDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Contests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupMembers]    Script Date: 5/27/2025 6:32:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupMembers](
	[GroupId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[JoinedAt] [datetime2](7) NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_GroupMembers] PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupPostFeedbackSummary]    Script Date: 5/27/2025 6:32:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupPostFeedbackSummary](
	[Id] [uniqueidentifier] NOT NULL,
	[GroupPostId] [uniqueidentifier] NOT NULL,
	[TotalLikes] [int] NOT NULL,
	[TotalComments] [int] NOT NULL,
	[AverageRating] [float] NOT NULL,
	[TotalRatings] [int] NOT NULL,
 CONSTRAINT [PK_GroupPostFeedbackSummary] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupPosts]    Script Date: 5/27/2025 6:32:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupPosts](
	[Id] [uniqueidentifier] NOT NULL,
	[GroupId] [uniqueidentifier] NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[Title] [nvarchar](1000) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[LastActivityDate] [datetime2](7) NULL,
	[ImgUrl] [nvarchar](255) NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_GroupPosts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Groups]    Script Date: 5/27/2025 6:32:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Groups](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[CategoryId] [uniqueidentifier] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[GroupAvatar] [nvarchar](500) NULL,
 CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Jobs]    Script Date: 5/27/2025 6:32:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Jobs](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[CompanyName] [nvarchar](255) NULL,
	[Location] [nvarchar](255) NOT NULL,
	[AddressDetail] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Requirements] [nvarchar](max) NOT NULL,
	[Benefits] [nvarchar](max) NOT NULL,
	[JobType] [nvarchar](50) NOT NULL,
	[SalaryFrom] [decimal](18, 2) NULL,
	[SalaryTo] [decimal](18, 2) NULL,
	[ExpiryDate] [datetime2](7) NULL,
	[ContactEmail] [nvarchar](255) NOT NULL,
	[ContactPhone] [nvarchar](20) NOT NULL,
	[PostedBy] [uniqueidentifier] NOT NULL,
	[CategoryId] [uniqueidentifier] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Jobs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LikeCommentGroupPost]    Script Date: 5/27/2025 6:32:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LikeCommentGroupPost](
	[Id] [uniqueidentifier] NOT NULL,
	[CommentGroupPostId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_LikeCommentGroupPost] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LikeGroupPost]    Script Date: 5/27/2025 6:32:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LikeGroupPost](
	[Id] [uniqueidentifier] NOT NULL,
	[GroupPostId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_LikeGroupPost] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 5/27/2025 6:32:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[Id] [uniqueidentifier] NOT NULL,
	[SenderId] [uniqueidentifier] NOT NULL,
	[ReceiverId] [uniqueidentifier] NOT NULL,
	[Content] [nvarchar](1000) NOT NULL,
	[SentAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notifications]    Script Date: 5/27/2025 6:32:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notifications](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Message] [nvarchar](1000) NOT NULL,
	[IsRead] [bit] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[ReadAt] [datetime2](7) NULL,
	[RelatedEntityId] [uniqueidentifier] NULL,
	[RelatedEntityType] [varchar](50) NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Type] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Notifications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RatingGroupPost]    Script Date: 5/27/2025 6:32:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RatingGroupPost](
	[Id] [uniqueidentifier] NOT NULL,
	[GroupPostId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Value] [int] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_RatingGroupPost] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ratings]    Script Date: 5/27/2025 6:32:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ratings](
	[Id] [uniqueidentifier] NOT NULL,
	[VideoId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Stars] [int] NOT NULL,
 CONSTRAINT [PK_Ratings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 5/27/2025 6:32:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 5/27/2025 6:32:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[FullName] [nvarchar](100) NULL,
	[Email] [nvarchar](100) NOT NULL,
	[PasswordHash] [nvarchar](max) NOT NULL,
	[Bio] [nvarchar](1000) NULL,
	[AvatarUrl] [nvarchar](500) NULL,
	[Location] [nvarchar](500) NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserTalents]    Script Date: 5/27/2025 6:32:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTalents](
	[UserId] [uniqueidentifier] NOT NULL,
	[CategoryId] [uniqueidentifier] NOT NULL,
	[SkillLevel] [nvarchar](max) NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_UserTalents] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VideoLikes]    Script Date: 5/27/2025 6:32:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VideoLikes](
	[Id] [uniqueidentifier] NOT NULL,
	[VideoId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[LikedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_VideoLikes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Videos]    Script Date: 5/27/2025 6:32:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Videos](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[Url] [nvarchar](500) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[CategoryId] [uniqueidentifier] NULL,
	[IsPrivate] [bit] NOT NULL,
	[UploadedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Videos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Views]    Script Date: 5/27/2025 6:32:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Views](
	[Id] [uniqueidentifier] NOT NULL,
	[VideoId] [uniqueidentifier] NOT NULL,
	[ViewerId] [uniqueidentifier] NOT NULL,
	[ViewedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Views] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Awards] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Awards] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Categories] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Certifications] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Certifications] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[CommentVideos] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[CommentVideos] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[ContestEntries] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[ContestEntries] ADD  DEFAULT (getdate()) FOR [SubmittedAt]
GO
ALTER TABLE [dbo].[Contests] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[GroupMembers] ADD  DEFAULT (getdate()) FOR [JoinedAt]
GO
ALTER TABLE [dbo].[GroupPostFeedbackSummary] ADD  DEFAULT ((0)) FOR [TotalLikes]
GO
ALTER TABLE [dbo].[GroupPostFeedbackSummary] ADD  DEFAULT ((0)) FOR [TotalComments]
GO
ALTER TABLE [dbo].[GroupPostFeedbackSummary] ADD  DEFAULT ((0.0000000000000000e+000)) FOR [AverageRating]
GO
ALTER TABLE [dbo].[GroupPostFeedbackSummary] ADD  DEFAULT ((0)) FOR [TotalRatings]
GO
ALTER TABLE [dbo].[GroupPosts] ADD  CONSTRAINT [DF__GroupPost__Title__2DE6D218]  DEFAULT (N'') FOR [Title]
GO
ALTER TABLE [dbo].[GroupPosts] ADD  CONSTRAINT [DF__GroupPost__Creat__0A9D95DB]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Groups] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Groups] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Jobs] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Jobs] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[LikeCommentGroupPost] ADD  DEFAULT (getutcdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[LikeGroupPost] ADD  DEFAULT (getutcdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Messages] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Messages] ADD  DEFAULT (getdate()) FOR [SentAt]
GO
ALTER TABLE [dbo].[Notifications] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsRead]
GO
ALTER TABLE [dbo].[Notifications] ADD  DEFAULT (getutcdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Notifications] ADD  DEFAULT (N'') FOR [Title]
GO
ALTER TABLE [dbo].[Notifications] ADD  DEFAULT (N'') FOR [Type]
GO
ALTER TABLE [dbo].[RatingGroupPost] ADD  DEFAULT ((0)) FOR [Value]
GO
ALTER TABLE [dbo].[RatingGroupPost] ADD  DEFAULT (getutcdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Ratings] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Roles] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[VideoLikes] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[VideoLikes] ADD  DEFAULT (getdate()) FOR [LikedAt]
GO
ALTER TABLE [dbo].[Videos] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Videos] ADD  DEFAULT (getdate()) FOR [UploadedAt]
GO
ALTER TABLE [dbo].[Views] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Views] ADD  DEFAULT (getdate()) FOR [ViewedAt]
GO
ALTER TABLE [dbo].[Awards]  WITH CHECK ADD  CONSTRAINT [FK_Awards_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Awards] CHECK CONSTRAINT [FK_Awards_Users_UserId]
GO
ALTER TABLE [dbo].[Certifications]  WITH CHECK ADD  CONSTRAINT [FK_Certifications_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Certifications] CHECK CONSTRAINT [FK_Certifications_Users_UserId]
GO
ALTER TABLE [dbo].[CommentGroupPosts]  WITH CHECK ADD  CONSTRAINT [FK_CommentGroupPosts_CommentGroupPosts_ParentCommentId] FOREIGN KEY([ParentCommentId])
REFERENCES [dbo].[CommentGroupPosts] ([Id])
GO
ALTER TABLE [dbo].[CommentGroupPosts] CHECK CONSTRAINT [FK_CommentGroupPosts_CommentGroupPosts_ParentCommentId]
GO
ALTER TABLE [dbo].[CommentGroupPosts]  WITH CHECK ADD  CONSTRAINT [FK_CommentGroupPosts_GroupPosts_GroupPostId] FOREIGN KEY([GroupPostId])
REFERENCES [dbo].[GroupPosts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CommentGroupPosts] CHECK CONSTRAINT [FK_CommentGroupPosts_GroupPosts_GroupPostId]
GO
ALTER TABLE [dbo].[CommentGroupPosts]  WITH CHECK ADD  CONSTRAINT [FK_CommentGroupPosts_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[CommentGroupPosts] CHECK CONSTRAINT [FK_CommentGroupPosts_Users_UserId]
GO
ALTER TABLE [dbo].[CommentVideos]  WITH CHECK ADD  CONSTRAINT [FK_CommentVideos_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CommentVideos] CHECK CONSTRAINT [FK_CommentVideos_Users_UserId]
GO
ALTER TABLE [dbo].[CommentVideos]  WITH CHECK ADD  CONSTRAINT [FK_CommentVideos_Videos_VideoId] FOREIGN KEY([VideoId])
REFERENCES [dbo].[Videos] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CommentVideos] CHECK CONSTRAINT [FK_CommentVideos_Videos_VideoId]
GO
ALTER TABLE [dbo].[ContestEntries]  WITH CHECK ADD  CONSTRAINT [FK_ContestEntries_Contests_ContestId] FOREIGN KEY([ContestId])
REFERENCES [dbo].[Contests] ([Id])
GO
ALTER TABLE [dbo].[ContestEntries] CHECK CONSTRAINT [FK_ContestEntries_Contests_ContestId]
GO
ALTER TABLE [dbo].[ContestEntries]  WITH CHECK ADD  CONSTRAINT [FK_ContestEntries_Videos_VideoId] FOREIGN KEY([VideoId])
REFERENCES [dbo].[Videos] ([Id])
GO
ALTER TABLE [dbo].[ContestEntries] CHECK CONSTRAINT [FK_ContestEntries_Videos_VideoId]
GO
ALTER TABLE [dbo].[Contests]  WITH CHECK ADD  CONSTRAINT [FK_Contests_Users_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Contests] CHECK CONSTRAINT [FK_Contests_Users_CreatedBy]
GO
ALTER TABLE [dbo].[GroupMembers]  WITH CHECK ADD  CONSTRAINT [FK_GroupMembers_Groups_GroupId] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Groups] ([Id])
GO
ALTER TABLE [dbo].[GroupMembers] CHECK CONSTRAINT [FK_GroupMembers_Groups_GroupId]
GO
ALTER TABLE [dbo].[GroupMembers]  WITH CHECK ADD  CONSTRAINT [FK_GroupMembers_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[GroupMembers] CHECK CONSTRAINT [FK_GroupMembers_Users_UserId]
GO
ALTER TABLE [dbo].[GroupPostFeedbackSummary]  WITH CHECK ADD  CONSTRAINT [FK_GroupPostFeedbackSummary_GroupPosts_GroupPostId] FOREIGN KEY([GroupPostId])
REFERENCES [dbo].[GroupPosts] ([Id])
GO
ALTER TABLE [dbo].[GroupPostFeedbackSummary] CHECK CONSTRAINT [FK_GroupPostFeedbackSummary_GroupPosts_GroupPostId]
GO
ALTER TABLE [dbo].[GroupPosts]  WITH CHECK ADD  CONSTRAINT [FK_GroupPosts_Groups_GroupId] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Groups] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GroupPosts] CHECK CONSTRAINT [FK_GroupPosts_Groups_GroupId]
GO
ALTER TABLE [dbo].[GroupPosts]  WITH CHECK ADD  CONSTRAINT [FK_GroupPosts_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GroupPosts] CHECK CONSTRAINT [FK_GroupPosts_Users_UserId]
GO
ALTER TABLE [dbo].[Groups]  WITH CHECK ADD  CONSTRAINT [FK_Groups_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[Groups] CHECK CONSTRAINT [FK_Groups_Categories_CategoryId]
GO
ALTER TABLE [dbo].[Groups]  WITH CHECK ADD  CONSTRAINT [FK_Groups_Users_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Groups] CHECK CONSTRAINT [FK_Groups_Users_CreatedBy]
GO
ALTER TABLE [dbo].[Jobs]  WITH CHECK ADD  CONSTRAINT [FK_Jobs_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Jobs] CHECK CONSTRAINT [FK_Jobs_Categories_CategoryId]
GO
ALTER TABLE [dbo].[Jobs]  WITH CHECK ADD  CONSTRAINT [FK_Jobs_Users_PostedBy] FOREIGN KEY([PostedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Jobs] CHECK CONSTRAINT [FK_Jobs_Users_PostedBy]
GO
ALTER TABLE [dbo].[LikeCommentGroupPost]  WITH CHECK ADD  CONSTRAINT [FK_LikeCommentGroupPost_CommentGroupPosts_CommentGroupPostId] FOREIGN KEY([CommentGroupPostId])
REFERENCES [dbo].[CommentGroupPosts] ([Id])
GO
ALTER TABLE [dbo].[LikeCommentGroupPost] CHECK CONSTRAINT [FK_LikeCommentGroupPost_CommentGroupPosts_CommentGroupPostId]
GO
ALTER TABLE [dbo].[LikeCommentGroupPost]  WITH CHECK ADD  CONSTRAINT [FK_LikeCommentGroupPost_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[LikeCommentGroupPost] CHECK CONSTRAINT [FK_LikeCommentGroupPost_Users_UserId]
GO
ALTER TABLE [dbo].[LikeGroupPost]  WITH CHECK ADD  CONSTRAINT [FK_LikeGroupPost_GroupPosts_GroupPostId] FOREIGN KEY([GroupPostId])
REFERENCES [dbo].[GroupPosts] ([Id])
GO
ALTER TABLE [dbo].[LikeGroupPost] CHECK CONSTRAINT [FK_LikeGroupPost_GroupPosts_GroupPostId]
GO
ALTER TABLE [dbo].[LikeGroupPost]  WITH CHECK ADD  CONSTRAINT [FK_LikeGroupPost_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[LikeGroupPost] CHECK CONSTRAINT [FK_LikeGroupPost_Users_UserId]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_Users_ReceiverId] FOREIGN KEY([ReceiverId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_Users_ReceiverId]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_Users_SenderId] FOREIGN KEY([SenderId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_Users_SenderId]
GO
ALTER TABLE [dbo].[Notifications]  WITH CHECK ADD  CONSTRAINT [FK_Notifications_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Notifications] CHECK CONSTRAINT [FK_Notifications_Users_UserId]
GO
ALTER TABLE [dbo].[RatingGroupPost]  WITH CHECK ADD  CONSTRAINT [FK_RatingGroupPost_GroupPosts_GroupPostId] FOREIGN KEY([GroupPostId])
REFERENCES [dbo].[GroupPosts] ([Id])
GO
ALTER TABLE [dbo].[RatingGroupPost] CHECK CONSTRAINT [FK_RatingGroupPost_GroupPosts_GroupPostId]
GO
ALTER TABLE [dbo].[RatingGroupPost]  WITH CHECK ADD  CONSTRAINT [FK_RatingGroupPost_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[RatingGroupPost] CHECK CONSTRAINT [FK_RatingGroupPost_Users_UserId]
GO
ALTER TABLE [dbo].[Ratings]  WITH CHECK ADD  CONSTRAINT [FK_Ratings_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Ratings] CHECK CONSTRAINT [FK_Ratings_Users_UserId]
GO
ALTER TABLE [dbo].[Ratings]  WITH CHECK ADD  CONSTRAINT [FK_Ratings_Videos_VideoId] FOREIGN KEY([VideoId])
REFERENCES [dbo].[Videos] ([Id])
GO
ALTER TABLE [dbo].[Ratings] CHECK CONSTRAINT [FK_Ratings_Videos_VideoId]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles_RoleId]
GO
ALTER TABLE [dbo].[UserTalents]  WITH CHECK ADD  CONSTRAINT [FK_UserTalents_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[UserTalents] CHECK CONSTRAINT [FK_UserTalents_Categories_CategoryId]
GO
ALTER TABLE [dbo].[UserTalents]  WITH CHECK ADD  CONSTRAINT [FK_UserTalents_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserTalents] CHECK CONSTRAINT [FK_UserTalents_Users_UserId]
GO
ALTER TABLE [dbo].[VideoLikes]  WITH CHECK ADD  CONSTRAINT [FK_VideoLikes_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[VideoLikes] CHECK CONSTRAINT [FK_VideoLikes_Users_UserId]
GO
ALTER TABLE [dbo].[VideoLikes]  WITH CHECK ADD  CONSTRAINT [FK_VideoLikes_Videos_VideoId] FOREIGN KEY([VideoId])
REFERENCES [dbo].[Videos] ([Id])
GO
ALTER TABLE [dbo].[VideoLikes] CHECK CONSTRAINT [FK_VideoLikes_Videos_VideoId]
GO
ALTER TABLE [dbo].[Videos]  WITH CHECK ADD  CONSTRAINT [FK_Videos_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[Videos] CHECK CONSTRAINT [FK_Videos_Categories_CategoryId]
GO
ALTER TABLE [dbo].[Videos]  WITH CHECK ADD  CONSTRAINT [FK_Videos_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Videos] CHECK CONSTRAINT [FK_Videos_Users_UserId]
GO
ALTER TABLE [dbo].[Views]  WITH CHECK ADD  CONSTRAINT [FK_Views_Users_ViewerId] FOREIGN KEY([ViewerId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Views] CHECK CONSTRAINT [FK_Views_Users_ViewerId]
GO
ALTER TABLE [dbo].[Views]  WITH CHECK ADD  CONSTRAINT [FK_Views_Videos_VideoId] FOREIGN KEY([VideoId])
REFERENCES [dbo].[Videos] ([Id])
GO
ALTER TABLE [dbo].[Views] CHECK CONSTRAINT [FK_Views_Videos_VideoId]
GO
