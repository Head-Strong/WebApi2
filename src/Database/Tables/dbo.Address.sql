/****** Object: Table [dbo].[Address] Script Date: 1/8/2017 2:57:39 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Address];


GO
CREATE TABLE [dbo].[Address] (
    [AddressId]  INT           IDENTITY (1, 1) NOT NULL,
    [CustomerId] INT           NOT NULL,
    [Pin]        NVARCHAR (50) NULL
);


