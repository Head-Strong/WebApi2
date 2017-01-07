/****** Object: Table [dbo].[Customer] Script Date: 1/8/2017 2:57:58 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Customer];


GO
CREATE TABLE [dbo].[Customer] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (50) NOT NULL,
    [LastName] NVARCHAR (50) NULL
);


