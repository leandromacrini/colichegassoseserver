
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/19/2015 12:14:08
-- Generated from EDMX file: C:\Users\esd81leamacr.ICC\documents\visual studio 2013\Projects\ColicheGassose\DataLayer\DataModel.edmx
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

IF OBJECT_ID(N'[dbo].[FK_NotificationAppointment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AppointmentSet] DROP CONSTRAINT [FK_NotificationAppointment];
GO
IF OBJECT_ID(N'[dbo].[FK_NotificationPillAlert]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PillAlertSet] DROP CONSTRAINT [FK_NotificationPillAlert];
GO
IF OBJECT_ID(N'[dbo].[FK_UserDataAppointment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AppointmentSet] DROP CONSTRAINT [FK_UserDataAppointment];
GO
IF OBJECT_ID(N'[dbo].[FK_UserDataSymptom]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SymptomSet] DROP CONSTRAINT [FK_UserDataSymptom];
GO
IF OBJECT_ID(N'[dbo].[FK_UserDataPillAlert]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PillAlertSet] DROP CONSTRAINT [FK_UserDataPillAlert];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[SymptomSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SymptomSet];
GO
IF OBJECT_ID(N'[dbo].[AppointmentSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AppointmentSet];
GO
IF OBJECT_ID(N'[dbo].[PillAlertSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PillAlertSet];
GO
IF OBJECT_ID(N'[dbo].[UserDataSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserDataSet];
GO
IF OBJECT_ID(N'[dbo].[NotificationSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NotificationSet];
GO
IF OBJECT_ID(N'[dbo].[WebUserSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WebUserSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'SymptomSet'
CREATE TABLE [dbo].[SymptomSet] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [App_Id] int  NOT NULL,
    [When] datetime  NOT NULL,
    [Pianto] bit  NOT NULL,
    [Rigurgito] bit  NOT NULL,
    [Agitazione] bit  NOT NULL,
    [Duration] int  NOT NULL,
    [Intensity] int  NOT NULL,
    [UserDataID] int  NOT NULL
);
GO

-- Creating table 'AppointmentSet'
CREATE TABLE [dbo].[AppointmentSet] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [App_Id] int  NOT NULL,
    [When] datetime  NOT NULL,
    [Info] nvarchar(max)  NULL,
    [UserDataID] int  NOT NULL,
    [Notification_ID] int  NULL
);
GO

-- Creating table 'PillAlertSet'
CREATE TABLE [dbo].[PillAlertSet] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [App_Id] int  NOT NULL,
    [PillId] int  NOT NULL,
    [When] datetime  NOT NULL,
    [Taken] bit  NULL,
    [Asked] bit  NULL,
    [ParentId] int  NULL,
    [UserDataID] int  NOT NULL,
    [Info] nvarchar(max)  NOT NULL,
    [Notification_ID] int  NULL
);
GO

-- Creating table 'UserDataSet'
CREATE TABLE [dbo].[UserDataSet] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [App_Id] int  NOT NULL,
    [Name] nvarchar(max)  NULL,
    [PatientPID] nvarchar(max)  NOT NULL,
    [DeviceToken] nvarchar(max)  NOT NULL,
    [OS] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'NotificationSet'
CREATE TABLE [dbo].[NotificationSet] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Status] int  NOT NULL,
    [When] datetime  NOT NULL,
    [Message] nvarchar(max)  NOT NULL,
    [DeviceToken] nvarchar(max)  NOT NULL,
    [DestinationOS] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'WebUserSet'
CREATE TABLE [dbo].[WebUserSet] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [User] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Admin] bit  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'SymptomSet'
ALTER TABLE [dbo].[SymptomSet]
ADD CONSTRAINT [PK_SymptomSet]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'AppointmentSet'
ALTER TABLE [dbo].[AppointmentSet]
ADD CONSTRAINT [PK_AppointmentSet]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'PillAlertSet'
ALTER TABLE [dbo].[PillAlertSet]
ADD CONSTRAINT [PK_PillAlertSet]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'UserDataSet'
ALTER TABLE [dbo].[UserDataSet]
ADD CONSTRAINT [PK_UserDataSet]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'NotificationSet'
ALTER TABLE [dbo].[NotificationSet]
ADD CONSTRAINT [PK_NotificationSet]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'WebUserSet'
ALTER TABLE [dbo].[WebUserSet]
ADD CONSTRAINT [PK_WebUserSet]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Notification_ID] in table 'AppointmentSet'
ALTER TABLE [dbo].[AppointmentSet]
ADD CONSTRAINT [FK_NotificationAppointment]
    FOREIGN KEY ([Notification_ID])
    REFERENCES [dbo].[NotificationSet]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NotificationAppointment'
CREATE INDEX [IX_FK_NotificationAppointment]
ON [dbo].[AppointmentSet]
    ([Notification_ID]);
GO

-- Creating foreign key on [Notification_ID] in table 'PillAlertSet'
ALTER TABLE [dbo].[PillAlertSet]
ADD CONSTRAINT [FK_NotificationPillAlert]
    FOREIGN KEY ([Notification_ID])
    REFERENCES [dbo].[NotificationSet]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NotificationPillAlert'
CREATE INDEX [IX_FK_NotificationPillAlert]
ON [dbo].[PillAlertSet]
    ([Notification_ID]);
GO

-- Creating foreign key on [UserDataID] in table 'AppointmentSet'
ALTER TABLE [dbo].[AppointmentSet]
ADD CONSTRAINT [FK_UserDataAppointment]
    FOREIGN KEY ([UserDataID])
    REFERENCES [dbo].[UserDataSet]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserDataAppointment'
CREATE INDEX [IX_FK_UserDataAppointment]
ON [dbo].[AppointmentSet]
    ([UserDataID]);
GO

-- Creating foreign key on [UserDataID] in table 'SymptomSet'
ALTER TABLE [dbo].[SymptomSet]
ADD CONSTRAINT [FK_UserDataSymptom]
    FOREIGN KEY ([UserDataID])
    REFERENCES [dbo].[UserDataSet]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserDataSymptom'
CREATE INDEX [IX_FK_UserDataSymptom]
ON [dbo].[SymptomSet]
    ([UserDataID]);
GO

-- Creating foreign key on [UserDataID] in table 'PillAlertSet'
ALTER TABLE [dbo].[PillAlertSet]
ADD CONSTRAINT [FK_UserDataPillAlert]
    FOREIGN KEY ([UserDataID])
    REFERENCES [dbo].[UserDataSet]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserDataPillAlert'
CREATE INDEX [IX_FK_UserDataPillAlert]
ON [dbo].[PillAlertSet]
    ([UserDataID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------