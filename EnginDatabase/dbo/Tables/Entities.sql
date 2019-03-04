﻿CREATE TABLE [dbo].[Entities] (
    [Id]        BIGINT         IDENTITY (1, 1) NOT NULL,
    [SiteId]    BIGINT         NOT NULL,
    [Name]      NVARCHAR (MAX) NULL,
    [CreatedOn] DATETIME       NOT NULL,
    [CreatedBy] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.Entities] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Entities_dbo.AspNetUsers_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_dbo.Entities_dbo.Sites_SiteId] FOREIGN KEY ([SiteId]) REFERENCES [dbo].[Sites] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_CreatedBy]
    ON [dbo].[Entities]([CreatedBy] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SiteId]
    ON [dbo].[Entities]([SiteId] ASC);

