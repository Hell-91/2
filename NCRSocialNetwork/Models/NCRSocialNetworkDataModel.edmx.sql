
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 10/05/2013 16:14:41
-- Generated from EDMX file: C:\Projects\NCRSocialNetwork\NCRSocialNetwork\Models\NCRSocialNetworkDataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [NCRSocialNetworkDatabase];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ClubClubSubscribe]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClubSubscribes] DROP CONSTRAINT [FK_ClubClubSubscribe];
GO
IF OBJECT_ID(N'[dbo].[FK_ClubEvent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Events] DROP CONSTRAINT [FK_ClubEvent];
GO
IF OBJECT_ID(N'[dbo].[FK_ClubEventRequest]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventRequests] DROP CONSTRAINT [FK_ClubEventRequest];
GO
IF OBJECT_ID(N'[dbo].[FK_EventEventLikeDislike]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventLikeDislikes] DROP CONSTRAINT [FK_EventEventLikeDislike];
GO
IF OBJECT_ID(N'[dbo].[FK_KeyClub]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Clubs] DROP CONSTRAINT [FK_KeyClub];
GO
IF OBJECT_ID(N'[dbo].[FK_KeyComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_KeyComment];
GO
IF OBJECT_ID(N'[dbo].[FK_KeyEvent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Events] DROP CONSTRAINT [FK_KeyEvent];
GO
IF OBJECT_ID(N'[dbo].[FK_UserClubModerator]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Clubs] DROP CONSTRAINT [FK_UserClubModerator];
GO
IF OBJECT_ID(N'[dbo].[FK_UserClubSubscribe]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClubSubscribes] DROP CONSTRAINT [FK_UserClubSubscribe];
GO
IF OBJECT_ID(N'[dbo].[FK_UserComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_UserComment];
GO
IF OBJECT_ID(N'[dbo].[FK_UserEventCreated]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Events] DROP CONSTRAINT [FK_UserEventCreated];
GO
IF OBJECT_ID(N'[dbo].[FK_UserEventLikeDislike]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventLikeDislikes] DROP CONSTRAINT [FK_UserEventLikeDislike];
GO
IF OBJECT_ID(N'[dbo].[FK_UserEventRequest]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventRequests] DROP CONSTRAINT [FK_UserEventRequest];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Clubs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Clubs];
GO
IF OBJECT_ID(N'[dbo].[ClubSubscribes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClubSubscribes];
GO
IF OBJECT_ID(N'[dbo].[Comments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Comments];
GO
IF OBJECT_ID(N'[dbo].[EventLikeDislikes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventLikeDislikes];
GO
IF OBJECT_ID(N'[dbo].[EventRequests]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventRequests];
GO
IF OBJECT_ID(N'[dbo].[Events]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Events];
GO
IF OBJECT_ID(N'[dbo].[Keys]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Keys];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserId] int IDENTITY(1,1) NOT NULL,
    [UserFirstName] nvarchar(50)  NOT NULL,
    [UserLastName] nvarchar(50)  NULL,
    [UserPassword] nvarchar(120)  NOT NULL,
    [UserRole] nvarchar(10)  NOT NULL,
    [UserEmail] nvarchar(max)  NULL,
    [UserContactNo] nvarchar(max)  NULL,
    [UserHobbies] nvarchar(max)  NULL,
    [UserTechnologies] nvarchar(max)  NULL,
    [UserDisplayPicture] nvarchar(max)  NULL
);
GO

-- Creating table 'Clubs'
CREATE TABLE [dbo].[Clubs] (
    [ClubId] int IDENTITY(1,1) NOT NULL,
    [ClubName] nvarchar(max)  NOT NULL,
    [ClubDescription] nvarchar(max)  NULL,
    [ClubModerator] int  NOT NULL,
    [ClubThumbnailPath] nvarchar(max)  NULL,
    [ClubImagePath] nvarchar(max)  NULL
);
GO

-- Creating table 'Events'
CREATE TABLE [dbo].[Events] (
    [EventId] int IDENTITY(1,1) NOT NULL,
    [EventName] nvarchar(max)  NOT NULL,
    [EventDescription] nvarchar(max)  NOT NULL,
    [EventClubId] int  NOT NULL,
    [EventVenue] nvarchar(max)  NULL,
    [EventDateTime] nvarchar(max)  NULL,
    [EventCreatedBy] int  NOT NULL,
    [EventCreatedTime] datetime  NOT NULL
);
GO

-- Creating table 'Keys'
CREATE TABLE [dbo].[Keys] (
    [KeyId] int IDENTITY(1,1) NOT NULL,
    [KeyType] nvarchar(5)  NOT NULL,
    [KeyCreatedDate] datetime  NOT NULL
);
GO

-- Creating table 'ClubSubscribes'
CREATE TABLE [dbo].[ClubSubscribes] (
    [ClubSubscribeId] int IDENTITY(1,1) NOT NULL,
    [ClubSubscribeClubId] int  NOT NULL,
    [ClubSubscribeUserId] int  NOT NULL
);
GO

-- Creating table 'Comments'
CREATE TABLE [dbo].[Comments] (
    [CommentId] int IDENTITY(1,1) NOT NULL,
    [CommentKeyId] int  NOT NULL,
    [CommentUserId] int  NOT NULL,
    [CommentDescription] nvarchar(max)  NOT NULL,
    [CommentCreatedDate] datetime  NOT NULL
);
GO

-- Creating table 'EventLikeDislikes'
CREATE TABLE [dbo].[EventLikeDislikes] (
    [EventLikeDislikeId] int IDENTITY(1,1) NOT NULL,
    [EventLikeDislikeEventId] int  NOT NULL,
    [EventLikeDislikeUserId] int  NOT NULL,
    [EventLikeDislikeValue] int  NOT NULL
);
GO

-- Creating table 'EventRequests'
CREATE TABLE [dbo].[EventRequests] (
    [EventRequestId] int IDENTITY(1,1) NOT NULL,
    [EventRequestEventTitle] nvarchar(max)  NOT NULL,
    [EventRequestEventDescription] nvarchar(max)  NOT NULL,
    [EventRequestClubId] int  NOT NULL,
    [EventRequestUserId] int  NOT NULL,
    [EventRequestDate] datetime  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [UserId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [ClubId] in table 'Clubs'
ALTER TABLE [dbo].[Clubs]
ADD CONSTRAINT [PK_Clubs]
    PRIMARY KEY CLUSTERED ([ClubId] ASC);
GO

-- Creating primary key on [EventId] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [PK_Events]
    PRIMARY KEY CLUSTERED ([EventId] ASC);
GO

-- Creating primary key on [KeyId] in table 'Keys'
ALTER TABLE [dbo].[Keys]
ADD CONSTRAINT [PK_Keys]
    PRIMARY KEY CLUSTERED ([KeyId] ASC);
GO

-- Creating primary key on [ClubSubscribeId] in table 'ClubSubscribes'
ALTER TABLE [dbo].[ClubSubscribes]
ADD CONSTRAINT [PK_ClubSubscribes]
    PRIMARY KEY CLUSTERED ([ClubSubscribeId] ASC);
GO

-- Creating primary key on [CommentId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [PK_Comments]
    PRIMARY KEY CLUSTERED ([CommentId] ASC);
GO

-- Creating primary key on [EventLikeDislikeId] in table 'EventLikeDislikes'
ALTER TABLE [dbo].[EventLikeDislikes]
ADD CONSTRAINT [PK_EventLikeDislikes]
    PRIMARY KEY CLUSTERED ([EventLikeDislikeId] ASC);
GO

-- Creating primary key on [EventRequestId] in table 'EventRequests'
ALTER TABLE [dbo].[EventRequests]
ADD CONSTRAINT [PK_EventRequests]
    PRIMARY KEY CLUSTERED ([EventRequestId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ClubModerator] in table 'Clubs'
ALTER TABLE [dbo].[Clubs]
ADD CONSTRAINT [FK_UserClubModerator]
    FOREIGN KEY ([ClubModerator])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserClubModerator'
CREATE INDEX [IX_FK_UserClubModerator]
ON [dbo].[Clubs]
    ([ClubModerator]);
GO

-- Creating foreign key on [EventCreatedBy] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [FK_UserEventCreated]
    FOREIGN KEY ([EventCreatedBy])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserEventCreated'
CREATE INDEX [IX_FK_UserEventCreated]
ON [dbo].[Events]
    ([EventCreatedBy]);
GO

-- Creating foreign key on [EventClubId] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [FK_ClubEvent]
    FOREIGN KEY ([EventClubId])
    REFERENCES [dbo].[Clubs]
        ([ClubId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ClubEvent'
CREATE INDEX [IX_FK_ClubEvent]
ON [dbo].[Events]
    ([EventClubId]);
GO

-- Creating foreign key on [ClubId] in table 'Clubs'
ALTER TABLE [dbo].[Clubs]
ADD CONSTRAINT [FK_KeyClub]
    FOREIGN KEY ([ClubId])
    REFERENCES [dbo].[Keys]
        ([KeyId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [EventId] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [FK_KeyEvent]
    FOREIGN KEY ([EventId])
    REFERENCES [dbo].[Keys]
        ([KeyId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [CommentKeyId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_KeyComment]
    FOREIGN KEY ([CommentKeyId])
    REFERENCES [dbo].[Keys]
        ([KeyId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_KeyComment'
CREATE INDEX [IX_FK_KeyComment]
ON [dbo].[Comments]
    ([CommentKeyId]);
GO

-- Creating foreign key on [CommentUserId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_UserComment]
    FOREIGN KEY ([CommentUserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserComment'
CREATE INDEX [IX_FK_UserComment]
ON [dbo].[Comments]
    ([CommentUserId]);
GO

-- Creating foreign key on [EventLikeDislikeEventId] in table 'EventLikeDislikes'
ALTER TABLE [dbo].[EventLikeDislikes]
ADD CONSTRAINT [FK_EventEventLikeDislike]
    FOREIGN KEY ([EventLikeDislikeEventId])
    REFERENCES [dbo].[Events]
        ([EventId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EventEventLikeDislike'
CREATE INDEX [IX_FK_EventEventLikeDislike]
ON [dbo].[EventLikeDislikes]
    ([EventLikeDislikeEventId]);
GO

-- Creating foreign key on [EventLikeDislikeUserId] in table 'EventLikeDislikes'
ALTER TABLE [dbo].[EventLikeDislikes]
ADD CONSTRAINT [FK_UserEventLikeDislike]
    FOREIGN KEY ([EventLikeDislikeUserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserEventLikeDislike'
CREATE INDEX [IX_FK_UserEventLikeDislike]
ON [dbo].[EventLikeDislikes]
    ([EventLikeDislikeUserId]);
GO

-- Creating foreign key on [EventRequestClubId] in table 'EventRequests'
ALTER TABLE [dbo].[EventRequests]
ADD CONSTRAINT [FK_ClubEventRequest]
    FOREIGN KEY ([EventRequestClubId])
    REFERENCES [dbo].[Clubs]
        ([ClubId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ClubEventRequest'
CREATE INDEX [IX_FK_ClubEventRequest]
ON [dbo].[EventRequests]
    ([EventRequestClubId]);
GO

-- Creating foreign key on [EventRequestUserId] in table 'EventRequests'
ALTER TABLE [dbo].[EventRequests]
ADD CONSTRAINT [FK_UserEventRequest]
    FOREIGN KEY ([EventRequestUserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserEventRequest'
CREATE INDEX [IX_FK_UserEventRequest]
ON [dbo].[EventRequests]
    ([EventRequestUserId]);
GO

-- Creating foreign key on [ClubSubscribeUserId] in table 'ClubSubscribes'
ALTER TABLE [dbo].[ClubSubscribes]
ADD CONSTRAINT [FK_UserClubSubscribe]
    FOREIGN KEY ([ClubSubscribeUserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserClubSubscribe'
CREATE INDEX [IX_FK_UserClubSubscribe]
ON [dbo].[ClubSubscribes]
    ([ClubSubscribeUserId]);
GO

-- Creating foreign key on [ClubSubscribeClubId] in table 'ClubSubscribes'
ALTER TABLE [dbo].[ClubSubscribes]
ADD CONSTRAINT [FK_ClubClubSubscribe]
    FOREIGN KEY ([ClubSubscribeClubId])
    REFERENCES [dbo].[Clubs]
        ([ClubId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ClubClubSubscribe'
CREATE INDEX [IX_FK_ClubClubSubscribe]
ON [dbo].[ClubSubscribes]
    ([ClubSubscribeClubId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------