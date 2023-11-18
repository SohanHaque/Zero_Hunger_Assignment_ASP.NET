
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/12/2023 12:52:31
-- Generated from EDMX file: F:\ASP Project\ASP.NET-Zero-Hunger-Assignment\Zero Hunger\DB\zero_hunger_model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Zero_Hunger];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_employee_admin]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[employee] DROP CONSTRAINT [FK_employee_admin];
GO
IF OBJECT_ID(N'[dbo].[FK_food_request]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[food] DROP CONSTRAINT [FK_food_request];
GO
IF OBJECT_ID(N'[dbo].[FK_request_admin]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[request] DROP CONSTRAINT [FK_request_admin];
GO
IF OBJECT_ID(N'[dbo].[FK_request_employee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[request] DROP CONSTRAINT [FK_request_employee];
GO
IF OBJECT_ID(N'[dbo].[FK_request_restaurant]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[request] DROP CONSTRAINT [FK_request_restaurant];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[admin]', 'U') IS NOT NULL
    DROP TABLE [dbo].[admin];
GO
IF OBJECT_ID(N'[dbo].[employee]', 'U') IS NOT NULL
    DROP TABLE [dbo].[employee];
GO
IF OBJECT_ID(N'[dbo].[food]', 'U') IS NOT NULL
    DROP TABLE [dbo].[food];
GO
IF OBJECT_ID(N'[dbo].[request]', 'U') IS NOT NULL
    DROP TABLE [dbo].[request];
GO
IF OBJECT_ID(N'[dbo].[restaurant]', 'U') IS NOT NULL
    DROP TABLE [dbo].[restaurant];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'admins'
CREATE TABLE [dbo].[admins] (
    [id] int IDENTITY(1,1) NOT NULL,
    [username] varchar(50)  NOT NULL,
    [password] varchar(50)  NOT NULL
);
GO

-- Creating table 'employees'
CREATE TABLE [dbo].[employees] (
    [id] int IDENTITY(1,1) NOT NULL,
    [username] varchar(50)  NOT NULL,
    [password] varchar(50)  NOT NULL,
    [name] varchar(50)  NOT NULL,
    [email] varchar(50)  NOT NULL,
    [phone] varchar(50)  NOT NULL,
    [address] varchar(50)  NOT NULL,
    [dob] datetime  NOT NULL,
    [admin_id] int  NULL
);
GO

-- Creating table 'foods'
CREATE TABLE [dbo].[foods] (
    [id] int IDENTITY(1,1) NOT NULL,
    [type] varchar(50)  NOT NULL,
    [quantity] int  NOT NULL,
    [request_id] int  NULL
);
GO

-- Creating table 'requests'
CREATE TABLE [dbo].[requests] (
    [id] int IDENTITY(1,1) NOT NULL,
    [status] varchar(50)  NOT NULL,
    [order_datetime] datetime  NOT NULL,
    [expire_datetime] datetime  NOT NULL,
    [total_quantity] int  NOT NULL,
    [restaurant_id] int  NULL,
    [employee_id] int  NULL,
    [admin_id] int  NULL
);
GO

-- Creating table 'restaurants'
CREATE TABLE [dbo].[restaurants] (
    [id] int IDENTITY(1,1) NOT NULL,
    [username] varchar(50)  NOT NULL,
    [password] varchar(50)  NOT NULL,
    [name] varchar(50)  NOT NULL,
    [email] varchar(50)  NOT NULL,
    [phone] varchar(50)  NOT NULL,
    [address] varchar(50)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'admins'
ALTER TABLE [dbo].[admins]
ADD CONSTRAINT [PK_admins]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'employees'
ALTER TABLE [dbo].[employees]
ADD CONSTRAINT [PK_employees]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'foods'
ALTER TABLE [dbo].[foods]
ADD CONSTRAINT [PK_foods]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'requests'
ALTER TABLE [dbo].[requests]
ADD CONSTRAINT [PK_requests]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'restaurants'
ALTER TABLE [dbo].[restaurants]
ADD CONSTRAINT [PK_restaurants]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [admin_id] in table 'employees'
ALTER TABLE [dbo].[employees]
ADD CONSTRAINT [FK_employee_admin]
    FOREIGN KEY ([admin_id])
    REFERENCES [dbo].[admins]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_employee_admin'
CREATE INDEX [IX_FK_employee_admin]
ON [dbo].[employees]
    ([admin_id]);
GO

-- Creating foreign key on [admin_id] in table 'requests'
ALTER TABLE [dbo].[requests]
ADD CONSTRAINT [FK_request_admin]
    FOREIGN KEY ([admin_id])
    REFERENCES [dbo].[admins]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_request_admin'
CREATE INDEX [IX_FK_request_admin]
ON [dbo].[requests]
    ([admin_id]);
GO

-- Creating foreign key on [employee_id] in table 'requests'
ALTER TABLE [dbo].[requests]
ADD CONSTRAINT [FK_request_employee]
    FOREIGN KEY ([employee_id])
    REFERENCES [dbo].[employees]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_request_employee'
CREATE INDEX [IX_FK_request_employee]
ON [dbo].[requests]
    ([employee_id]);
GO

-- Creating foreign key on [request_id] in table 'foods'
ALTER TABLE [dbo].[foods]
ADD CONSTRAINT [FK_food_request]
    FOREIGN KEY ([request_id])
    REFERENCES [dbo].[requests]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_food_request'
CREATE INDEX [IX_FK_food_request]
ON [dbo].[foods]
    ([request_id]);
GO

-- Creating foreign key on [restaurant_id] in table 'requests'
ALTER TABLE [dbo].[requests]
ADD CONSTRAINT [FK_request_restaurant]
    FOREIGN KEY ([restaurant_id])
    REFERENCES [dbo].[restaurants]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_request_restaurant'
CREATE INDEX [IX_FK_request_restaurant]
ON [dbo].[requests]
    ([restaurant_id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------