CREATE TABLE [dbo].[REF_NatureMatiere] (
    [Id]              BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]            NVARCHAR (MAX) NULL,
    [CreatedOn]       DATETIME       NOT NULL,
    [CreatedBy]       NVARCHAR (128) NOT NULL,
    [TypeCheckListId] BIGINT         NOT NULL,
    CONSTRAINT [PK_dbo.REF_NatureMatiere] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.NatureMatiere_dbo.AspNetUsers_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_dbo.NatureMatiere_dbo.TypeCheckList_TypeCheckListId] FOREIGN KEY ([TypeCheckListId]) REFERENCES [dbo].[REF_TypeCheckList] ([Id])
);

