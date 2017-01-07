/****** Object: Table [dbo].[Profiles] Script Date: 1/8/2017 2:58:30 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Profiles];


GO
CREATE TABLE [dbo].[Profiles] (
    [idProfile]   INT           IDENTITY (1, 1) NOT NULL,
    [profileName] NVARCHAR (50) NULL
);


