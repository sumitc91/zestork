
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 03/26/2014 12:30:35
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

IF OBJECT_ID(N'[dbo].[FK_UsersValidateUserKey]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ValidateUserKeys] DROP CONSTRAINT [FK_UsersValidateUserKey];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[ValidateUserKeys]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ValidateUserKeys];
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
    [Uid] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ValidateUserKeys'
CREATE TABLE [dbo].[ValidateUserKeys] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [guid] nvarchar(max)  NOT NULL,
    [UsersId] int  NOT NULL
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

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UsersId] in table 'ValidateUserKeys'
ALTER TABLE [dbo].[ValidateUserKeys]
ADD CONSTRAINT [FK_UsersValidateUserKey]
    FOREIGN KEY ([UsersId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UsersValidateUserKey'
CREATE INDEX [IX_FK_UsersValidateUserKey]
ON [dbo].[ValidateUserKeys]
    ([UsersId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------