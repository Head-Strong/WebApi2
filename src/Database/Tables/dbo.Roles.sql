/****** Object: Table [dbo].[Roles] Script Date: 1/8/2017 2:58:43 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Roles];


GO
CREATE TABLE [dbo].[Roles] (
    [idRole]   INT          IDENTITY (1, 1) NOT NULL,
    [roleName] VARCHAR (50) NULL
);


