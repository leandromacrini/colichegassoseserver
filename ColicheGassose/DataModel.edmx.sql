
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/19/2014 10:17:21
-- Generated from EDMX file: C:\Users\esd81leamacr.ICC\Documents\Visual Studio 2013\Projects\ColicheGassose\ColicheGassose\DataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [colichegassose];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'SymptomSet'
CREATE TABLE [dbo].[SymptomSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [App_Id] int  NOT NULL,
    [When] datetime  NOT NULL,
    [Pianto] bit  NOT NULL,
    [Rigurgito] bit  NOT NULL,
    [Agitazione] bit  NOT NULL,
    [Duration] int  NOT NULL,
    [Intensity] int  NOT NULL
);
GO

-- Creating table 'AppointmentSet'
CREATE TABLE [dbo].[AppointmentSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [App_Id] int  NOT NULL,
    [When] datetime  NOT NULL,
    [Info] nvarchar(max)  NULL
);
GO

-- Creating table 'PillSet'
CREATE TABLE [dbo].[PillSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [App_Id] int  NOT NULL,
    [Shape] int  NULL,
    [Color] int  NULL,
    [Info] int  NULL,
    [Deletable] bit  NOT NULL
);
GO

-- Creating table 'PillAlertSet'
CREATE TABLE [dbo].[PillAlertSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [App_Id] int  NOT NULL,
    [PillId] int  NOT NULL,
    [When] datetime  NOT NULL,
    [Taken] bit  NULL,
    [Asked] bit  NULL,
    [Notified] int  NULL
);
GO

-- Creating table 'UserDataSet'
CREATE TABLE [dbo].[UserDataSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [App_Id] int  NOT NULL,
    [Name] nvarchar(max)  NULL,
    [PatientPID] nvarchar(max)  NOT NULL,
    [DeviceToken] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'SymptomSet'
ALTER TABLE [dbo].[SymptomSet]
ADD CONSTRAINT [PK_SymptomSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AppointmentSet'
ALTER TABLE [dbo].[AppointmentSet]
ADD CONSTRAINT [PK_AppointmentSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PillSet'
ALTER TABLE [dbo].[PillSet]
ADD CONSTRAINT [PK_PillSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PillAlertSet'
ALTER TABLE [dbo].[PillAlertSet]
ADD CONSTRAINT [PK_PillAlertSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserDataSet'
ALTER TABLE [dbo].[UserDataSet]
ADD CONSTRAINT [PK_UserDataSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------