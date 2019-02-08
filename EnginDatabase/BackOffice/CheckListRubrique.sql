CREATE TABLE [dbo].[CheckListRubrique] (
    [Id]              BIGINT         IDENTITY (1, 1) NOT NULL,
    [TypeCheckListId] INT            NOT NULL,
    [Name]            NVARCHAR (MAX) NOT NULL,
    [ShowOrder]       INT            NOT NULL,
    [IsActif]         BIT            NOT NULL,
    [CreatedOn]       DATETIME       NOT NULL,
    [CreatedBy]       NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.CheckListRubrique] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.CheckListRubrique_dbo.AspNetUsers_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_dbo.CheckListRubrique_dbo.TypeCheckList_TypeCheckListId] FOREIGN KEY ([TypeCheckListId]) REFERENCES [dbo].[TypeCheckList] ([Id])
);



GO
CREATE NONCLUSTERED INDEX [IX_TypeCheckListId]
    ON [dbo].[CheckListRubrique]([TypeCheckListId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CreatedBy]
    ON [dbo].[CheckListRubrique]([CreatedBy] ASC);

