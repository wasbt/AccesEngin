CREATE TABLE [dbo].[Sites] (
    [Id]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (MAX) NULL,
    [Address]      NVARCHAR (MAX) NULL,
    [PhoneNumber1] NVARCHAR (MAX) NULL,
    [PhoneNumber2] NVARCHAR (MAX) NULL,
    [CreatedOn]    DATETIME       NOT NULL,
    [CreatedBy]    NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.Sites] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Sites_dbo.AspNetUsers_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_CreatedBy]
    ON [dbo].[Sites]([CreatedBy] ASC);

