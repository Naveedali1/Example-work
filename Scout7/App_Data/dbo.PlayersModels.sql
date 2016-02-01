CREATE TABLE [dbo].[PlayersModels] (
    [ID]          INT IDENTITY (1, 1) NOT NULL,
    [FirstName]   NVARCHAR (MAX) NULL,
    [Surname]     NVARCHAR (MAX) NULL,
    [DateOfBirth] DATETIME       NULL,
    [CurrentClub] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.PlayersModels] PRIMARY KEY CLUSTERED ([ID] ASC)
);

