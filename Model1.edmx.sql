
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/07/2022 13:22:39
-- Generated from EDMX file: N:\Programming\College\ForestMarathone\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ForestMarathones];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Administrator_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Administrator] DROP CONSTRAINT [FK_Administrator_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_MarathonResults_MarathonStatus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MarathonResults] DROP CONSTRAINT [FK_MarathonResults_MarathonStatus];
GO
IF OBJECT_ID(N'[dbo].[FK_MarathonResults_Participant]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MarathonResults] DROP CONSTRAINT [FK_MarathonResults_Participant];
GO
IF OBJECT_ID(N'[dbo].[FK_MarathonResults_Sets]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MarathonResults] DROP CONSTRAINT [FK_MarathonResults_Sets];
GO
IF OBJECT_ID(N'[dbo].[FK_MarathonResults_TypeDistances]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MarathonResults] DROP CONSTRAINT [FK_MarathonResults_TypeDistances];
GO
IF OBJECT_ID(N'[dbo].[FK_Participant_Countries]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Participant] DROP CONSTRAINT [FK_Participant_Countries];
GO
IF OBJECT_ID(N'[dbo].[FK_Participant_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Participant] DROP CONSTRAINT [FK_Participant_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_SponsoredParticipants_Participant]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SponsoredParticipants] DROP CONSTRAINT [FK_SponsoredParticipants_Participant];
GO
IF OBJECT_ID(N'[dbo].[FK_SponsoredParticipants_Sponsors]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SponsoredParticipants] DROP CONSTRAINT [FK_SponsoredParticipants_Sponsors];
GO
IF OBJECT_ID(N'[dbo].[FK_Sponsors_Sponsors]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sponsors] DROP CONSTRAINT [FK_Sponsors_Sponsors];
GO
IF OBJECT_ID(N'[dbo].[FK_Sponsors_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sponsors] DROP CONSTRAINT [FK_Sponsors_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_Users_Roles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_Users_Roles];
GO
IF OBJECT_ID(N'[dbo].[FK_Users_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_Users_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_Users_UserStatus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_Users_UserStatus];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Administrator]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Administrator];
GO
IF OBJECT_ID(N'[dbo].[Countries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Countries];
GO
IF OBJECT_ID(N'[dbo].[LoginHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LoginHistory];
GO
IF OBJECT_ID(N'[dbo].[MarathonResults]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MarathonResults];
GO
IF OBJECT_ID(N'[dbo].[MarathonStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MarathonStatus];
GO
IF OBJECT_ID(N'[dbo].[Participant]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Participant];
GO
IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[Sets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sets];
GO
IF OBJECT_ID(N'[dbo].[SponsoredParticipants]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SponsoredParticipants];
GO
IF OBJECT_ID(N'[dbo].[Sponsors]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sponsors];
GO
IF OBJECT_ID(N'[dbo].[TypeDistances]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TypeDistances];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[UserStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserStatus];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Countries'
CREATE TABLE [dbo].[Countries] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(20)  NOT NULL
);
GO

-- Creating table 'LoginHistory'
CREATE TABLE [dbo].[LoginHistory] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Login] varchar(64)  NOT NULL,
    [IPAddress] varchar(15)  NOT NULL,
    [ConnectionTime] time  NOT NULL,
    [ValidAuth] bit  NOT NULL
);
GO

-- Creating table 'MarathonResults'
CREATE TABLE [dbo].[MarathonResults] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RunnerId] int  NOT NULL,
    [MarathonTypeId] int  NOT NULL,
    [SetId] int  NOT NULL,
    [StatusId] int  NOT NULL,
    [TimeRun] decimal(18,2)  NOT NULL,
    [Plate] int  NOT NULL
);
GO

-- Creating table 'MarathonStatus'
CREATE TABLE [dbo].[MarathonStatus] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(64)  NOT NULL
);
GO

-- Creating table 'Participant'
CREATE TABLE [dbo].[Participant] (
    [Id] int  NOT NULL,
    [FirstName] nvarchar(30)  NOT NULL,
    [MiddleName] nvarchar(30)  NULL,
    [LastName] nvarchar(30)  NOT NULL,
    [Sex] bit  NOT NULL,
    [Birthday] datetime  NOT NULL,
    [CountryId] int  NOT NULL,
    [Phone] char(15)  NOT NULL,
    [Wallet] decimal(18,0)  NOT NULL,
    [Photo] varbinary(max)  NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(15)  NOT NULL
);
GO

-- Creating table 'Sets'
CREATE TABLE [dbo].[Sets] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(64)  NOT NULL,
    [Price] decimal(18,2)  NOT NULL
);
GO

-- Creating table 'SponsoredParticipants'
CREATE TABLE [dbo].[SponsoredParticipants] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ParticipantId] int  NOT NULL,
    [SponsorId] int  NOT NULL,
    [SponsoredMoney] decimal(18,2)  NOT NULL
);
GO

-- Creating table 'Sponsors'
CREATE TABLE [dbo].[Sponsors] (
    [Id] int  NOT NULL,
    [FirstName] nvarchar(30)  NOT NULL,
    [MiddleName] nvarchar(30)  NULL,
    [LastName] nvarchar(30)  NOT NULL,
    [CountryId] int  NOT NULL,
    [CardValidMonth] int  NOT NULL,
    [CardValidYear] int  NOT NULL,
    [TotalSum] decimal(18,0)  NOT NULL,
    [Logo] varbinary(max)  NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'TypeDistances'
CREATE TABLE [dbo].[TypeDistances] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(64)  NOT NULL,
    [Price] decimal(18,2)  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Login] varchar(64)  NOT NULL,
    [Password] varchar(20)  NOT NULL,
    [RoleId] int  NOT NULL,
    [UserStatusId] int  NOT NULL,
    [Email] varchar(30)  NOT NULL
);
GO

-- Creating table 'Administrator'
CREATE TABLE [dbo].[Administrator] (
    [Id] int  NOT NULL,
    [FirstName] nvarchar(30)  NOT NULL,
    [MiddleName] nvarchar(30)  NULL,
    [LastName] nvarchar(30)  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'UserStatus'
CREATE TABLE [dbo].[UserStatus] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(15)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Countries'
ALTER TABLE [dbo].[Countries]
ADD CONSTRAINT [PK_Countries]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LoginHistory'
ALTER TABLE [dbo].[LoginHistory]
ADD CONSTRAINT [PK_LoginHistory]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MarathonResults'
ALTER TABLE [dbo].[MarathonResults]
ADD CONSTRAINT [PK_MarathonResults]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MarathonStatus'
ALTER TABLE [dbo].[MarathonStatus]
ADD CONSTRAINT [PK_MarathonStatus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Participant'
ALTER TABLE [dbo].[Participant]
ADD CONSTRAINT [PK_Participant]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Sets'
ALTER TABLE [dbo].[Sets]
ADD CONSTRAINT [PK_Sets]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SponsoredParticipants'
ALTER TABLE [dbo].[SponsoredParticipants]
ADD CONSTRAINT [PK_SponsoredParticipants]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Sponsors'
ALTER TABLE [dbo].[Sponsors]
ADD CONSTRAINT [PK_Sponsors]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [Id] in table 'TypeDistances'
ALTER TABLE [dbo].[TypeDistances]
ADD CONSTRAINT [PK_TypeDistances]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Administrator'
ALTER TABLE [dbo].[Administrator]
ADD CONSTRAINT [PK_Administrator]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserStatus'
ALTER TABLE [dbo].[UserStatus]
ADD CONSTRAINT [PK_UserStatus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CountryId] in table 'Participant'
ALTER TABLE [dbo].[Participant]
ADD CONSTRAINT [FK_Participant_Countries]
    FOREIGN KEY ([CountryId])
    REFERENCES [dbo].[Countries]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Participant_Countries'
CREATE INDEX [IX_FK_Participant_Countries]
ON [dbo].[Participant]
    ([CountryId]);
GO

-- Creating foreign key on [CountryId] in table 'Sponsors'
ALTER TABLE [dbo].[Sponsors]
ADD CONSTRAINT [FK_Sponsors_Sponsors]
    FOREIGN KEY ([CountryId])
    REFERENCES [dbo].[Countries]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Sponsors_Sponsors'
CREATE INDEX [IX_FK_Sponsors_Sponsors]
ON [dbo].[Sponsors]
    ([CountryId]);
GO

-- Creating foreign key on [StatusId] in table 'MarathonResults'
ALTER TABLE [dbo].[MarathonResults]
ADD CONSTRAINT [FK_MarathonResults_MarathonStatus]
    FOREIGN KEY ([StatusId])
    REFERENCES [dbo].[MarathonStatus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MarathonResults_MarathonStatus'
CREATE INDEX [IX_FK_MarathonResults_MarathonStatus]
ON [dbo].[MarathonResults]
    ([StatusId]);
GO

-- Creating foreign key on [RunnerId] in table 'MarathonResults'
ALTER TABLE [dbo].[MarathonResults]
ADD CONSTRAINT [FK_MarathonResults_Participant]
    FOREIGN KEY ([RunnerId])
    REFERENCES [dbo].[Participant]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MarathonResults_Participant'
CREATE INDEX [IX_FK_MarathonResults_Participant]
ON [dbo].[MarathonResults]
    ([RunnerId]);
GO

-- Creating foreign key on [SetId] in table 'MarathonResults'
ALTER TABLE [dbo].[MarathonResults]
ADD CONSTRAINT [FK_MarathonResults_Sets]
    FOREIGN KEY ([SetId])
    REFERENCES [dbo].[Sets]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MarathonResults_Sets'
CREATE INDEX [IX_FK_MarathonResults_Sets]
ON [dbo].[MarathonResults]
    ([SetId]);
GO

-- Creating foreign key on [MarathonTypeId] in table 'MarathonResults'
ALTER TABLE [dbo].[MarathonResults]
ADD CONSTRAINT [FK_MarathonResults_TypeDistances]
    FOREIGN KEY ([MarathonTypeId])
    REFERENCES [dbo].[TypeDistances]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MarathonResults_TypeDistances'
CREATE INDEX [IX_FK_MarathonResults_TypeDistances]
ON [dbo].[MarathonResults]
    ([MarathonTypeId]);
GO

-- Creating foreign key on [ParticipantId] in table 'SponsoredParticipants'
ALTER TABLE [dbo].[SponsoredParticipants]
ADD CONSTRAINT [FK_SponsoredParticipants_Participant]
    FOREIGN KEY ([ParticipantId])
    REFERENCES [dbo].[Participant]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SponsoredParticipants_Participant'
CREATE INDEX [IX_FK_SponsoredParticipants_Participant]
ON [dbo].[SponsoredParticipants]
    ([ParticipantId]);
GO

-- Creating foreign key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_Users_Participant]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Participant]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [RoleId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_Users_Roles]
    FOREIGN KEY ([RoleId])
    REFERENCES [dbo].[Roles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Users_Roles'
CREATE INDEX [IX_FK_Users_Roles]
ON [dbo].[Users]
    ([RoleId]);
GO

-- Creating foreign key on [SponsorId] in table 'SponsoredParticipants'
ALTER TABLE [dbo].[SponsoredParticipants]
ADD CONSTRAINT [FK_SponsoredParticipants_Sponsors]
    FOREIGN KEY ([SponsorId])
    REFERENCES [dbo].[Sponsors]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SponsoredParticipants_Sponsors'
CREATE INDEX [IX_FK_SponsoredParticipants_Sponsors]
ON [dbo].[SponsoredParticipants]
    ([SponsorId]);
GO

-- Creating foreign key on [UserId] in table 'Administrator'
ALTER TABLE [dbo].[Administrator]
ADD CONSTRAINT [FK_Administrator_Users]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Administrator_Users'
CREATE INDEX [IX_FK_Administrator_Users]
ON [dbo].[Administrator]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'Participant'
ALTER TABLE [dbo].[Participant]
ADD CONSTRAINT [FK_Participant_Users]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Participant_Users'
CREATE INDEX [IX_FK_Participant_Users]
ON [dbo].[Participant]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'Sponsors'
ALTER TABLE [dbo].[Sponsors]
ADD CONSTRAINT [FK_Sponsors_Users]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Sponsors_Users'
CREATE INDEX [IX_FK_Sponsors_Users]
ON [dbo].[Sponsors]
    ([UserId]);
GO

-- Creating foreign key on [UserStatusId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_Users_UserStatus]
    FOREIGN KEY ([UserStatusId])
    REFERENCES [dbo].[UserStatus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Users_UserStatus'
CREATE INDEX [IX_FK_Users_UserStatus]
ON [dbo].[Users]
    ([UserStatusId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------