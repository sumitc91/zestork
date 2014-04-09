
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 04/09/2014 13:33:08
-- Generated from EDMX file: F:\pcongo\tags\zestork\zestork\Models\Zestork.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Zestork];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[ValidateUserKeys]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ValidateUserKeys];
GO
IF OBJECT_ID(N'[dbo].[ThirdPartyLogins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ThirdPartyLogins];
GO
IF OBJECT_ID(N'[dbo].[LinkedInAuthApiDatas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LinkedInAuthApiDatas];
GO
IF OBJECT_ID(N'[dbo].[ClientDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClientDetails];
GO
IF OBJECT_ID(N'[dbo].[UserSkills]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSkills];
GO
IF OBJECT_ID(N'[dbo].[UserDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserDetails];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [isActive] nvarchar(max)  NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [Source] nvarchar(max)  NOT NULL,
    [guid] nvarchar(max)  NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [ImageUrl] nvarchar(max)  NOT NULL,
    [gender] nvarchar(max)  NOT NULL,
    [Locked] nvarchar(max)  NULL
);
GO

-- Creating table 'ValidateUserKeys'
CREATE TABLE [dbo].[ValidateUserKeys] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [guid] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ThirdPartyLogins'
CREATE TABLE [dbo].[ThirdPartyLogins] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [FacebookId] nvarchar(max)  NOT NULL,
    [FacebookAccessToken] nvarchar(max)  NOT NULL,
    [GoogleId] nvarchar(max)  NOT NULL,
    [GoogleAccessToken] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'LinkedInAuthApiDatas'
CREATE TABLE [dbo].[LinkedInAuthApiDatas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [oauth_Token] nvarchar(max)  NOT NULL,
    [oauth_TokenSecret] nvarchar(max)  NOT NULL,
    [oauth_verifier] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ClientDetails'
CREATE TABLE [dbo].[ClientDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CompanyName] nvarchar(max)  NULL,
    [Username] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'UserSkills'
CREATE TABLE [dbo].[UserSkills] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [Skill] nvarchar(max)  NOT NULL,
    [Rating] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'UserDetails'
CREATE TABLE [dbo].[UserDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [Country] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'UserPageSettings'
CREATE TABLE [dbo].[UserPageSettings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [PageThemeColor] nvarchar(max)  NULL,
    [LayoutWidth] nvarchar(max)  NULL,
    [TopBar] nvarchar(max)  NULL,
    [SideBar] nvarchar(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ValidateUserKeys'
ALTER TABLE [dbo].[ValidateUserKeys]
ADD CONSTRAINT [PK_ValidateUserKeys]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ThirdPartyLogins'
ALTER TABLE [dbo].[ThirdPartyLogins]
ADD CONSTRAINT [PK_ThirdPartyLogins]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LinkedInAuthApiDatas'
ALTER TABLE [dbo].[LinkedInAuthApiDatas]
ADD CONSTRAINT [PK_LinkedInAuthApiDatas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ClientDetails'
ALTER TABLE [dbo].[ClientDetails]
ADD CONSTRAINT [PK_ClientDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserSkills'
ALTER TABLE [dbo].[UserSkills]
ADD CONSTRAINT [PK_UserSkills]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserDetails'
ALTER TABLE [dbo].[UserDetails]
ADD CONSTRAINT [PK_UserDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserPageSettings'
ALTER TABLE [dbo].[UserPageSettings]
ADD CONSTRAINT [PK_UserPageSettings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------