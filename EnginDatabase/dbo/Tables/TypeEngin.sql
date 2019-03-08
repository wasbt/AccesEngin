CREATE TABLE [dbo].[TypeEngin] (
    [Id]              BIGINT         IDENTITY (1, 1) NOT NULL,
    [TypeCheckListId] BIGINT         NOT NULL,
    [Name]            NVARCHAR (MAX) NOT NULL,
    [DureeEstimative] NVARCHAR (MAX) NULL,
    [CreatedOn]       DATETIME       NOT NULL,
    [CreatedBy]       NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.TypeEngin] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.TypeEngin_dbo.AspNetUsers_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_dbo.TypeEngin_dbo.TypeCheckList_TypeCheckListId] FOREIGN KEY ([TypeCheckListId]) REFERENCES [dbo].[TypeCheckList] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_CreatedBy]
    ON [dbo].[TypeEngin]([CreatedBy] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TypeCheckListId]
    ON [dbo].[TypeEngin]([TypeCheckListId] ASC);

