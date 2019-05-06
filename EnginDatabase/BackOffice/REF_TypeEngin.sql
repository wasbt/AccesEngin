CREATE TABLE [dbo].[REF_TypeEngin] (
    [Id]              BIGINT         IDENTITY (1, 1) NOT NULL,
    [TypeCheckListId] BIGINT         NOT NULL,
    [Name]            NVARCHAR (MAX) NOT NULL,
    [DureeEstimative] NVARCHAR (MAX) NULL,
    [CreatedOn]       DATETIME       NOT NULL,
    [CreatedBy]       NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.REF_TypeEngin] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.TypeEngin_dbo.AspNetUsers_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_dbo.TypeEngin_dbo.TypeCheckList_TypeCheckListId] FOREIGN KEY ([TypeCheckListId]) REFERENCES [dbo].[REF_TypeCheckList] ([Id])
);

