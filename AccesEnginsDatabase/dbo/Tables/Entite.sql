CREATE TABLE [dbo].[Entite] (
    [Id]                      INT         IDENTITY (1, 1) NOT NULL,
    [SiteId]                  BIGINT         NOT NULL,
    [Name]                    NVARCHAR (MAX) NULL,
    [CreatedOn]               DATETIME       NOT NULL,
    [CreatedBy]               NVARCHAR (128) NOT NULL,
    [HSEEntiteUserId]         NVARCHAR (128) NOT NULL,
    [ADFUserId]               NVARCHAR (MAX) NULL,
    [ResponsableEntiteUserId] NVARCHAR (MAX) NULL,
    [IsHSE]                   BIT            NULL,
    CONSTRAINT [PK_dbo.Entitie] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Entitie_dbo.AspNetUsers_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_dbo.Entitie_dbo.Profile_HSEEntiteUserId] FOREIGN KEY ([HSEEntiteUserId]) REFERENCES [dbo].[Profile] ([Id]),
    CONSTRAINT [FK_dbo.Entitie_dbo.Sites_SiteId] FOREIGN KEY ([SiteId]) REFERENCES [dbo].[Sites] ([Id])
);

