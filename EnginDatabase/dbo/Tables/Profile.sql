CREATE TABLE [dbo].[Profile] (
    [Id]               NVARCHAR (128) NOT NULL,
    [FullName]         NVARCHAR (MAX) NULL,
    [Email]            NVARCHAR (MAX) NULL,
    [Phone]            NVARCHAR (MAX) NULL,
    [Details]          NVARCHAR (MAX) NULL,
    [DtLastConnection] DATETIME       NULL,
    [EntiteId]         BIGINT         NULL,
    CONSTRAINT [PK_dbo.Profile] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Profile_dbo.AspNetUsers_Id] FOREIGN KEY ([Id]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_dbo.Profile_dbo.Entities_EntiteId] FOREIGN KEY ([EntiteId]) REFERENCES [dbo].[Entities] ([Id])
);






GO
CREATE NONCLUSTERED INDEX [IX_Id]
    ON [dbo].[Profile]([Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_EntiteId]
    ON [dbo].[Profile]([EntiteId] ASC);

