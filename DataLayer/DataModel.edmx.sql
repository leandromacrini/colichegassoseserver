




-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 02/19/2015 18:03:48
-- Generated from EDMX file: C:\Users\esd81leamacr.ICC\documents\visual studio 2013\Projects\ColicheGassose\DataLayer\DataModel.edmx
-- Target version: 3.0.0.0
-- --------------------------------------------------

DROP DATABASE IF EXISTS `colichegassose`;
CREATE DATABASE `colichegassose`;
USE `colichegassose`;

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

--    ALTER TABLE `AppointmentSet` DROP CONSTRAINT `FK_NotificationAppointment`;
--    ALTER TABLE `PillAlertSet` DROP CONSTRAINT `FK_NotificationPillAlert`;
--    ALTER TABLE `AppointmentSet` DROP CONSTRAINT `FK_UserDataAppointment`;
--    ALTER TABLE `SymptomSet` DROP CONSTRAINT `FK_UserDataSymptom`;
--    ALTER TABLE `PillAlertSet` DROP CONSTRAINT `FK_UserDataPillAlert`;

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
    DROP TABLE IF EXISTS `SymptomSet`;
    DROP TABLE IF EXISTS `AppointmentSet`;
    DROP TABLE IF EXISTS `PillAlertSet`;
    DROP TABLE IF EXISTS `UserDataSet`;
    DROP TABLE IF EXISTS `NotificationSet`;
    DROP TABLE IF EXISTS `WebUserSet`;
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

CREATE TABLE `SymptomSet`(
	`ID` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`App_Id` int NOT NULL, 
	`When` datetime NOT NULL, 
	`Pianto` bool NOT NULL, 
	`Rigurgito` bool NOT NULL, 
	`Agitazione` bool NOT NULL, 
	`Duration` int NOT NULL, 
	`Intensity` int NOT NULL, 
	`UserDataID` int NOT NULL);

ALTER TABLE `SymptomSet` ADD PRIMARY KEY (ID);




CREATE TABLE `AppointmentSet`(
	`ID` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`App_Id` int NOT NULL, 
	`When` datetime NOT NULL, 
	`Info` longtext, 
	`UserDataID` int NOT NULL, 
	`Notification_ID` int);

ALTER TABLE `AppointmentSet` ADD PRIMARY KEY (ID);




CREATE TABLE `PillAlertSet`(
	`ID` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`App_Id` int NOT NULL, 
	`PillId` int NOT NULL, 
	`When` datetime NOT NULL, 
	`Taken` bool, 
	`Asked` bool, 
	`ParentId` int, 
	`UserDataID` int NOT NULL, 
	`Info` longtext NOT NULL, 
	`Notification_ID` int);

ALTER TABLE `PillAlertSet` ADD PRIMARY KEY (ID);




CREATE TABLE `UserDataSet`(
	`ID` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`App_Id` int NOT NULL, 
	`Name` longtext, 
	`PatientPID` longtext NOT NULL, 
	`DeviceToken` longtext NOT NULL, 
	`OS` longtext NOT NULL);

ALTER TABLE `UserDataSet` ADD PRIMARY KEY (ID);




CREATE TABLE `NotificationSet`(
	`ID` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Status` int NOT NULL, 
	`When` datetime NOT NULL, 
	`Message` longtext NOT NULL, 
	`DeviceToken` longtext NOT NULL, 
	`DestinationOS` longtext NOT NULL);

ALTER TABLE `NotificationSet` ADD PRIMARY KEY (ID);




CREATE TABLE `WebUserSet`(
	`ID` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`User` longtext NOT NULL, 
	`Password` longtext NOT NULL, 
	`Admin` bool NOT NULL);

ALTER TABLE `WebUserSet` ADD PRIMARY KEY (ID);






-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on `Notification_ID` in table 'AppointmentSet'

ALTER TABLE `AppointmentSet`
ADD CONSTRAINT `FK_NotificationAppointment`
    FOREIGN KEY (`Notification_ID`)
    REFERENCES `NotificationSet`
        (`ID`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_NotificationAppointment'

CREATE INDEX `IX_FK_NotificationAppointment` 
    ON `AppointmentSet`
    (`Notification_ID`);

-- Creating foreign key on `Notification_ID` in table 'PillAlertSet'

ALTER TABLE `PillAlertSet`
ADD CONSTRAINT `FK_NotificationPillAlert`
    FOREIGN KEY (`Notification_ID`)
    REFERENCES `NotificationSet`
        (`ID`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_NotificationPillAlert'

CREATE INDEX `IX_FK_NotificationPillAlert` 
    ON `PillAlertSet`
    (`Notification_ID`);

-- Creating foreign key on `UserDataID` in table 'AppointmentSet'

ALTER TABLE `AppointmentSet`
ADD CONSTRAINT `FK_UserDataAppointment`
    FOREIGN KEY (`UserDataID`)
    REFERENCES `UserDataSet`
        (`ID`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserDataAppointment'

CREATE INDEX `IX_FK_UserDataAppointment` 
    ON `AppointmentSet`
    (`UserDataID`);

-- Creating foreign key on `UserDataID` in table 'SymptomSet'

ALTER TABLE `SymptomSet`
ADD CONSTRAINT `FK_UserDataSymptom`
    FOREIGN KEY (`UserDataID`)
    REFERENCES `UserDataSet`
        (`ID`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserDataSymptom'

CREATE INDEX `IX_FK_UserDataSymptom` 
    ON `SymptomSet`
    (`UserDataID`);

-- Creating foreign key on `UserDataID` in table 'PillAlertSet'

ALTER TABLE `PillAlertSet`
ADD CONSTRAINT `FK_UserDataPillAlert`
    FOREIGN KEY (`UserDataID`)
    REFERENCES `UserDataSet`
        (`ID`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserDataPillAlert'

CREATE INDEX `IX_FK_UserDataPillAlert` 
    ON `PillAlertSet`
    (`UserDataID`);

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
