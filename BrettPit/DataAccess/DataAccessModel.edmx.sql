
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/25/2016 12:57:25
-- Generated from EDMX file: C:\src\BrettPit\BrettPit\BrettPit\DataAccess\DataAccessModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DEV_BrettPit];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__elo__gid__1DE57479]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[eloes] DROP CONSTRAINT [FK__elo__gid__1DE57479];
GO
IF OBJECT_ID(N'[dbo].[FK__elo__uid__1CF15040]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[eloes] DROP CONSTRAINT [FK__elo__uid__1CF15040];
GO
IF OBJECT_ID(N'[dbo].[FK__log_admin__uid_a__20C1E124]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[log_admin] DROP CONSTRAINT [FK__log_admin__uid_a__20C1E124];
GO
IF OBJECT_ID(N'[dbo].[FK__log_admin__uid_p__21B6055D]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[log_admin] DROP CONSTRAINT [FK__log_admin__uid_p__21B6055D];
GO
IF OBJECT_ID(N'[dbo].[FK__log_pwcha__admin__24927208]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[log_pwchange] DROP CONSTRAINT [FK__log_pwcha__admin__24927208];
GO
IF OBJECT_ID(N'[dbo].[FK__log_pwchan__uid___239E4DCF]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[log_pwchange] DROP CONSTRAINT [FK__log_pwchan__uid___239E4DCF];
GO
IF OBJECT_ID(N'[dbo].[FK__pairings__game_s__164452B1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[pairings] DROP CONSTRAINT [FK__pairings__game_s__164452B1];
GO
IF OBJECT_ID(N'[dbo].[FK__pairings__uid1__145C0A3F]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[pairings] DROP CONSTRAINT [FK__pairings__uid1__145C0A3F];
GO
IF OBJECT_ID(N'[dbo].[FK__pairings__uid2__15502E78]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[pairings] DROP CONSTRAINT [FK__pairings__uid2__15502E78];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[eloes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[eloes];
GO
IF OBJECT_ID(N'[dbo].[game_systems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[game_systems];
GO
IF OBJECT_ID(N'[dbo].[log_admin]', 'U') IS NOT NULL
    DROP TABLE [dbo].[log_admin];
GO
IF OBJECT_ID(N'[dbo].[log_pwchange]', 'U') IS NOT NULL
    DROP TABLE [dbo].[log_pwchange];
GO
IF OBJECT_ID(N'[dbo].[pairings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[pairings];
GO
IF OBJECT_ID(N'[dbo].[users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'eloes'
CREATE TABLE [dbo].[eloes] (
    [uid] int  NOT NULL,
    [gid] int  NOT NULL,
    [elo] int  NOT NULL,
    [id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'game_systems'
CREATE TABLE [dbo].[game_systems] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] varchar(255)  NULL,
    [description] varchar(255)  NULL
);
GO

-- Creating table 'log_admin'
CREATE TABLE [dbo].[log_admin] (
    [id] int IDENTITY(1,1) NOT NULL,
    [timestamp] datetime  NOT NULL,
    [uid_affected] int  NOT NULL,
    [uid_performed] int  NOT NULL,
    [changed_from] varchar(255)  NOT NULL,
    [changed_to] varchar(255)  NOT NULL
);
GO

-- Creating table 'log_pwchange'
CREATE TABLE [dbo].[log_pwchange] (
    [id] int IDENTITY(1,1) NOT NULL,
    [timestamp] datetime  NOT NULL,
    [uid_] int  NOT NULL,
    [admin_id] int  NOT NULL,
    [pw_old] varchar(255)  NOT NULL,
    [pw_new] varchar(255)  NOT NULL
);
GO

-- Creating table 'pairings'
CREATE TABLE [dbo].[pairings] (
    [id] int IDENTITY(1,1) NOT NULL,
    [timestamp] datetime  NOT NULL,
    [uid1] int  NOT NULL,
    [status1] int  NOT NULL,
    [uid2] int  NOT NULL,
    [status2] int  NOT NULL,
    [game_system_id] int  NOT NULL,
    [result] int  NOT NULL
);
GO

-- Creating table 'users'
CREATE TABLE [dbo].[users] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] varchar(255)  NOT NULL,
    [password] varchar(255)  NOT NULL,
    [email] varchar(255)  NOT NULL,
    [isadmin] bit  NOT NULL,
    [last_login] datetime  NOT NULL,
    [loginid] varchar(255)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'eloes'
ALTER TABLE [dbo].[eloes]
ADD CONSTRAINT [PK_eloes]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'game_systems'
ALTER TABLE [dbo].[game_systems]
ADD CONSTRAINT [PK_game_systems]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'log_admin'
ALTER TABLE [dbo].[log_admin]
ADD CONSTRAINT [PK_log_admin]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'log_pwchange'
ALTER TABLE [dbo].[log_pwchange]
ADD CONSTRAINT [PK_log_pwchange]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'pairings'
ALTER TABLE [dbo].[pairings]
ADD CONSTRAINT [PK_pairings]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'users'
ALTER TABLE [dbo].[users]
ADD CONSTRAINT [PK_users]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [gid] in table 'eloes'
ALTER TABLE [dbo].[eloes]
ADD CONSTRAINT [FK__elo__gid__1DE57479]
    FOREIGN KEY ([gid])
    REFERENCES [dbo].[game_systems]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__elo__gid__1DE57479'
CREATE INDEX [IX_FK__elo__gid__1DE57479]
ON [dbo].[eloes]
    ([gid]);
GO

-- Creating foreign key on [uid] in table 'eloes'
ALTER TABLE [dbo].[eloes]
ADD CONSTRAINT [FK__elo__uid__1CF15040]
    FOREIGN KEY ([uid])
    REFERENCES [dbo].[users]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__elo__uid__1CF15040'
CREATE INDEX [IX_FK__elo__uid__1CF15040]
ON [dbo].[eloes]
    ([uid]);
GO

-- Creating foreign key on [game_system_id] in table 'pairings'
ALTER TABLE [dbo].[pairings]
ADD CONSTRAINT [FK__pairings__game_s__164452B1]
    FOREIGN KEY ([game_system_id])
    REFERENCES [dbo].[game_systems]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__pairings__game_s__164452B1'
CREATE INDEX [IX_FK__pairings__game_s__164452B1]
ON [dbo].[pairings]
    ([game_system_id]);
GO

-- Creating foreign key on [uid_affected] in table 'log_admin'
ALTER TABLE [dbo].[log_admin]
ADD CONSTRAINT [FK__log_admin__uid_a__20C1E124]
    FOREIGN KEY ([uid_affected])
    REFERENCES [dbo].[users]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__log_admin__uid_a__20C1E124'
CREATE INDEX [IX_FK__log_admin__uid_a__20C1E124]
ON [dbo].[log_admin]
    ([uid_affected]);
GO

-- Creating foreign key on [uid_performed] in table 'log_admin'
ALTER TABLE [dbo].[log_admin]
ADD CONSTRAINT [FK__log_admin__uid_p__21B6055D]
    FOREIGN KEY ([uid_performed])
    REFERENCES [dbo].[users]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__log_admin__uid_p__21B6055D'
CREATE INDEX [IX_FK__log_admin__uid_p__21B6055D]
ON [dbo].[log_admin]
    ([uid_performed]);
GO

-- Creating foreign key on [admin_id] in table 'log_pwchange'
ALTER TABLE [dbo].[log_pwchange]
ADD CONSTRAINT [FK__log_pwcha__admin__24927208]
    FOREIGN KEY ([admin_id])
    REFERENCES [dbo].[users]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__log_pwcha__admin__24927208'
CREATE INDEX [IX_FK__log_pwcha__admin__24927208]
ON [dbo].[log_pwchange]
    ([admin_id]);
GO

-- Creating foreign key on [uid_] in table 'log_pwchange'
ALTER TABLE [dbo].[log_pwchange]
ADD CONSTRAINT [FK__log_pwchan__uid___239E4DCF]
    FOREIGN KEY ([uid_])
    REFERENCES [dbo].[users]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__log_pwchan__uid___239E4DCF'
CREATE INDEX [IX_FK__log_pwchan__uid___239E4DCF]
ON [dbo].[log_pwchange]
    ([uid_]);
GO

-- Creating foreign key on [uid1] in table 'pairings'
ALTER TABLE [dbo].[pairings]
ADD CONSTRAINT [FK__pairings__uid1__145C0A3F]
    FOREIGN KEY ([uid1])
    REFERENCES [dbo].[users]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__pairings__uid1__145C0A3F'
CREATE INDEX [IX_FK__pairings__uid1__145C0A3F]
ON [dbo].[pairings]
    ([uid1]);
GO

-- Creating foreign key on [uid2] in table 'pairings'
ALTER TABLE [dbo].[pairings]
ADD CONSTRAINT [FK__pairings__uid2__15502E78]
    FOREIGN KEY ([uid2])
    REFERENCES [dbo].[users]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__pairings__uid2__15502E78'
CREATE INDEX [IX_FK__pairings__uid2__15502E78]
ON [dbo].[pairings]
    ([uid2]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------