CREATE TABLE [dbo].[CheckListExigence] (
    [Id]                  BIGINT         IDENTITY (1, 1) NOT NULL,
    [CheckListRubriqueId] BIGINT         NOT NULL,
    [Name]                NVARCHAR (MAX) NOT NULL,
    [ShowOrder]           INT            NOT NULL,
    [IsActif]             BIT            NOT NULL,
    [Poids]               FLOAT (53)     NOT NULL,
    [CreatedOn]           DATETIME       NOT NULL,
    [CreatedBy]           NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.CheckListExigence] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.CheckListExigence_dbo.AspNetUsers_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_dbo.CheckListExigence_dbo.CheckListRubrique_CheckListRubriqueId] FOREIGN KEY ([CheckListRubriqueId]) REFERENCES [dbo].[CheckListRubrique] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_CreatedBy]
    ON [dbo].[CheckListExigence]([CreatedBy] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CheckListRubriqueId]
    ON [dbo].[CheckListExigence]([CheckListRubriqueId] ASC);

