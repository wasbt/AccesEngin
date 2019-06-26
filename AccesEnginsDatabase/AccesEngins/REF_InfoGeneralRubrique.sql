CREATE TABLE [AccesEngins].[REF_InfoGeneralRubrique] (
    [Id]        BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (MAX) NOT NULL,
    [ShowOrder] INT            NOT NULL,
    [IsActif]   BIT            NOT NULL,
    [CreatedOn] DATETIME       NOT NULL,
    [CreatedBy] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.REF_InfoGeneralRubrique] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccesEngins.InfoGeneralRubrique_AccesEngins.AspNetUsers_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

