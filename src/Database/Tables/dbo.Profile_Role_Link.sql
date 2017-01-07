/****** Object: Table [dbo].[Profile_Role_Link] Script Date: 1/8/2017 2:58:15 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Profile_Role_Link];


GO
CREATE TABLE [dbo].[Profile_Role_Link] (
    [idProfile] INT NOT NULL,
    [idRole]    INT NOT NULL
);


