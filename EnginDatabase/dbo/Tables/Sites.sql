CREATE TABLE [dbo].[Sites] (
    [Id]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (MAX) NULL,
    [Address]      NVARCHAR (MAX) NULL,
    [PhoneNumber1] NVARCHAR (MAX) NULL,
    [PhoneNumber2] NVARCHAR (MAX) NULL,
    [CreatedOn]    DATETIME       NOT NULL,
    [CreatedBy]    NVARCHAR (128) NOT NULL,
    [HSESiteId]    NVARCHAR (128) DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_dbo.Sites] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Sites_dbo.AspNetUsers_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_dbo.Sites_dbo.Profile_HSESiteId] FOREIGN KEY ([HSESiteId]) REFERENCES [dbo].[Profile] ([Id])
);






GO



GO


