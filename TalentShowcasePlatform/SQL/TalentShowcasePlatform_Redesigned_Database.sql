

CREATE TABLE [Users] (
    [Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [UserName] NVARCHAR(100),
    [Email] NVARCHAR(100),
    [PasswordHash] NVARCHAR(MAX),
    [Bio] NVARCHAR(1000),
    [AvatarUrl] NVARCHAR(500),
    [RoleId] UNIQUEIDENTIFIER,
    [CreatedAt] DATETIME DEFAULT GETDATE()
);

CREATE TABLE [Roles] (
    [Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [Name] NVARCHAR(50)
);

CREATE TABLE [Categories] (
    [Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [Name] NVARCHAR(100),
    [Description] NVARCHAR(255)
);

CREATE TABLE [UserTalents] (
    [UserId] UNIQUEIDENTIFIER,
    [CategoryId] UNIQUEIDENTIFIER,
    PRIMARY KEY ([UserId], [CategoryId]),
    FOREIGN KEY ([UserId]) REFERENCES [Users]([Id]),
    FOREIGN KEY ([CategoryId]) REFERENCES [Categories]([Id])
);

CREATE TABLE [Videos] (
    [Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [Title] NVARCHAR(200),
    [Description] NVARCHAR(1000),
    [Url] NVARCHAR(500),
    [UserId] UNIQUEIDENTIFIER,
    [CategoryId] UNIQUEIDENTIFIER,
    [IsPrivate] BIT,  -- Sử dụng BIT cho boolean trong SQL Server
    [UploadedAt] DATETIME DEFAULT GETDATE(),
    FOREIGN KEY ([UserId]) REFERENCES [Users]([Id]),
    FOREIGN KEY ([CategoryId]) REFERENCES [Categories]([Id])
);

CREATE TABLE [Comments] (
    [Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [VideoId] UNIQUEIDENTIFIER,
    [UserId] UNIQUEIDENTIFIER,
    [Content] NVARCHAR(1000),
    [CreatedAt] DATETIME DEFAULT GETDATE(),
    FOREIGN KEY ([VideoId]) REFERENCES [Videos]([Id]),
    FOREIGN KEY ([UserId]) REFERENCES [Users]([Id])
);

CREATE TABLE [Ratings] (
    [Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [VideoId] UNIQUEIDENTIFIER,
    [UserId] UNIQUEIDENTIFIER,
    [Stars] INT,
    FOREIGN KEY ([VideoId]) REFERENCES [Videos]([Id]),
    FOREIGN KEY ([UserId]) REFERENCES [Users]([Id])
);

CREATE TABLE [Groups] (
    [Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [Name] NVARCHAR(100),
    [Description] NVARCHAR(500),
    [CreatedBy] UNIQUEIDENTIFIER,
    [CreatedAt] DATETIME DEFAULT GETDATE(),
    FOREIGN KEY ([CreatedBy]) REFERENCES [Users]([Id])
);

CREATE TABLE [GroupMembers] (
    [GroupId] UNIQUEIDENTIFIER,
    [UserId] UNIQUEIDENTIFIER,
    [JoinedAt] DATETIME DEFAULT GETDATE(),
    PRIMARY KEY ([GroupId], [UserId]),
    FOREIGN KEY ([GroupId]) REFERENCES [Groups]([Id]),
    FOREIGN KEY ([UserId]) REFERENCES [Users]([Id])
);

CREATE TABLE [Contests] (
    [Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [Title] NVARCHAR(200),
    [Description] NVARCHAR(1000),
    [CreatedBy] UNIQUEIDENTIFIER,
    [StartDate] DATETIME,
    [EndDate] DATETIME,
    FOREIGN KEY ([CreatedBy]) REFERENCES [Users]([Id])
);

CREATE TABLE [ContestEntries] (
    [Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [ContestId] UNIQUEIDENTIFIER,
    [VideoId] UNIQUEIDENTIFIER,
    [SubmittedAt] DATETIME DEFAULT GETDATE(),
    [Votes] INT,
    FOREIGN KEY ([ContestId]) REFERENCES [Contests]([Id]),
    FOREIGN KEY ([VideoId]) REFERENCES [Videos]([Id])
);

CREATE TABLE [Messages] (
    [Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [SenderId] UNIQUEIDENTIFIER,
    [ReceiverId] UNIQUEIDENTIFIER,
    [Content] NVARCHAR(1000),
    [SentAt] DATETIME DEFAULT GETDATE(),
    FOREIGN KEY ([SenderId]) REFERENCES [Users]([Id]),
    FOREIGN KEY ([ReceiverId]) REFERENCES [Users]([Id])
);

CREATE TABLE [Notifications] (
    [Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [UserId] UNIQUEIDENTIFIER,
    [Message] NVARCHAR(500),
    [IsRead] BIT, -- Sử dụng BIT cho boolean trong SQL Server
    [CreatedAt] DATETIME DEFAULT GETDATE(),
    FOREIGN KEY ([UserId]) REFERENCES [Users]([Id])
);

CREATE TABLE [Views] (
    [Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [VideoId] UNIQUEIDENTIFIER,
    [ViewerId] UNIQUEIDENTIFIER,
    [ViewedAt] DATETIME DEFAULT GETDATE(),
    FOREIGN KEY ([VideoId]) REFERENCES [Videos]([Id]),
    FOREIGN KEY ([ViewerId]) REFERENCES [Users]([Id])
);

CREATE TABLE [Jobs] (
    [Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [Title] NVARCHAR(200) NOT NULL,
    [Description] NVARCHAR(1000) NOT NULL,
    [PostedBy] UNIQUEIDENTIFIER NOT NULL,
    [CategoryId] UNIQUEIDENTIFIER NOT NULL,
    [Location] NVARCHAR(255),
    [JobType] NVARCHAR(50),  -- Ví dụ: 'Full-time', 'Part-time', 'Freelance'
    [SalaryFrom] DECIMAL(10, 2),
    [SalaryTo] DECIMAL(10, 2),
    [ExpiryDate] DATETIME,
    [ContactEmail] NVARCHAR(100), -- Hoặc ContactPhone, hoặc cả hai
    [CreatedAt] DATETIME DEFAULT GETDATE(),
    FOREIGN KEY ([PostedBy]) REFERENCES [Users]([Id]),
    FOREIGN KEY ([CategoryId]) REFERENCES [Categories]([Id])
);

CREATE TABLE [Payments] (
    [Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [SenderId] UNIQUEIDENTIFIER,
    [ReceiverId] UNIQUEIDENTIFIER,
    [Amount] DECIMAL(10, 2), --Sửa ở đây
    [Message] NVARCHAR(200),
    [CreatedAt] DATETIME DEFAULT GETDATE(),
    FOREIGN KEY ([SenderId]) REFERENCES [Users]([Id]),
    FOREIGN KEY ([ReceiverId]) REFERENCES [Users]([Id])
);

CREATE TABLE [VideoLikes] (
    [Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [VideoId] UNIQUEIDENTIFIER,
    [UserId] UNIQUEIDENTIFIER,
    [LikedAt] DATETIME DEFAULT GETDATE(),
    FOREIGN KEY ([UserId]) REFERENCES [Users]([Id]),
    FOREIGN KEY ([VideoId]) REFERENCES [Videos]([Id])
);

CREATE TABLE [Achievements] (  -- Bảng mới
    [Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    [Title] NVARCHAR(255) NOT NULL,
    [Description] NVARCHAR(1000),
    [DateAchieved] DATE,
    [CreatedAt] DATETIME DEFAULT GETDATE(),
    FOREIGN KEY ([UserId]) REFERENCES [Users]([Id])
);

CREATE TABLE [Awards] (  -- Bảng mới
    [Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    [Title] NVARCHAR(255) NOT NULL,
    [AwardingOrganization] NVARCHAR(255),
    [DateReceived] DATE,
    [CreatedAt] DATETIME DEFAULT GETDATE(),
    FOREIGN KEY ([UserId]) REFERENCES [Users]([Id])
);

CREATE TABLE [Certifications] (  -- Bảng mới
    [Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    [Title] NVARCHAR(255) NOT NULL,
    [IssuingAuthority] NVARCHAR(255),
    [IssueDate] DATE,
    [ExpiryDate] DATE,
    [CreatedAt] DATETIME DEFAULT GETDATE(),
    FOREIGN KEY ([UserId]) REFERENCES [Users]([Id])
);

