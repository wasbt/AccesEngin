CREATE TABLE [dbo].[REF_CheckListRubrique] (
    [Id]              BIGINT         IDENTITY (1, 1) NOT NULL,
    [TypeCheckListId] BIGINT         NOT NULL,
    [Name]            NVARCHAR (MAX) NOT NULL,
    [ShowOrder]       INT            NOT NULL,
    [IsActif]         BIT            NOT NULL,
    [CreatedOn]       DATETIME       NOT NULL,
    [CreatedBy]       NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.REF_CheckListRubrique] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.CheckListRubrique_dbo.AspNetUsers_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_dbo.CheckListRubrique_dbo.TypeCheckList_TypeCheckListId] FOREIGN KEY ([TypeCheckListId]) REFERENCES [dbo].[REF_TypeCheckList] ([Id])
);

