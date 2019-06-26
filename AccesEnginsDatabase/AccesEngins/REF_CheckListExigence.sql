CREATE TABLE [AccesEngins].[REF_CheckListExigence] (
    [Id]                  BIGINT         IDENTITY (1, 1) NOT NULL,
    [CheckListRubriqueId] BIGINT         NOT NULL,
    [Name]                NVARCHAR (MAX) NOT NULL,
    [ShowOrder]           INT            NOT NULL,
    [IsActif]             BIT            NOT NULL,
    [CreatedOn]           DATETIME       NOT NULL,
    [CreatedBy]           NVARCHAR (128) NOT NULL,
    [IsHasDate]           BIT            DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_dbo.REF_CheckListExigence] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.CheckListExigence_dbo.AspNetUsers_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_AccesEngins.CheckListExigence_AccesEngins.CheckListRubrique_CheckListRubriqueId] FOREIGN KEY ([CheckListRubriqueId]) REFERENCES [AccesEngins].[REF_CheckListRubrique] ([Id])
);

