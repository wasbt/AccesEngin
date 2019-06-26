CREATE TABLE [AccesEngins].[ResultatControleEntete] (
    [Id]                  BIGINT         IDENTITY (1, 1) NOT NULL,
    [DemandeAccesEnginId] BIGINT         NOT NULL,
    [CreatedBy]           NVARCHAR (128) NOT NULL,
    [CreatedOn]           DATETIME       NOT NULL,
    [AppFileId]           BIGINT         NULL,
    [Data] VARBINARY(MAX) NULL, 
    CONSTRAINT [PK_dbo.ResultatControleEntete] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.ResultatControleEntete_dbo.AppFiles_AppFileId] FOREIGN KEY ([AppFileId]) REFERENCES [dbo].[AppFile] ([AppFileId]),
    CONSTRAINT [FK_dbo.ResultatControleEntete_dbo.AspNetUsers_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_dbo.ResultatControleEntete_dbo.DemandeAccesEngin_DemandeAccesEnginId] FOREIGN KEY ([DemandeAccesEnginId]) REFERENCES [AccesEngins].[DemandeAccesEngin] ([Id])
);

