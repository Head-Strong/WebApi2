/****** Object: Table [dbo].[Logs] Script Date: 1/8/2017 2:55:30 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Logs];


GO
CREATE TABLE [dbo].[Logs] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    [Message]         NVARCHAR (MAX) NULL,
    [MessageTemplate] NVARCHAR (MAX) NULL,
    [Level]           NVARCHAR (128) NULL,
    [TimeStamp]       DATETIME       NOT NULL,
    [Exception]       NVARCHAR (MAX) NULL,
    [Properties]      XML            NULL,
    [LogEvent]        NVARCHAR (MAX) NULL,
    [User]            NVARCHAR (50)  NULL,
    [Enviornment]     NVARCHAR (50)  NULL,
    [Host]            NVARCHAR (50)  NULL,
    [Request]         NVARCHAR (MAX) NULL,
    [Response]        NVARCHAR (MAX) NULL
);


