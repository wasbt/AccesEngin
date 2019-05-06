CREATE TABLE [dbo].[REF_TypeCheckList] (
    [Id]        BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (MAX) NOT NULL,
    [CreatedOn] DATETIME       NOT NULL,
    [CreatedBy] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.REF_TypeCheckList] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.TypeCheckList_dbo.AspNetUsers_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

