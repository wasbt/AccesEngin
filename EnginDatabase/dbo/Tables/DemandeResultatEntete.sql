CREATE TABLE [dbo].[DemandeResultatEntete] (
    [Id]                  BIGINT         IDENTITY (1, 1) NOT NULL,
    [DemandeAccesEnginId] BIGINT         NOT NULL,
    [CreatedBy]           NVARCHAR (128) NOT NULL,
    [CreatedOn]           DATETIME       NOT NULL,
    [AppFileId]           BIGINT         NULL,
    CONSTRAINT [PK_dbo.DemandeResultatEntete] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.DemandeResultatEntete_dbo.AppFiles_AppFileId] FOREIGN KEY ([AppFileId]) REFERENCES [dbo].[AppFiles] ([Id]),
    CONSTRAINT [FK_dbo.DemandeResultatEntete_dbo.AspNetUsers_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_dbo.DemandeResultatEntete_dbo.DemandeAccesEngin_DemandeAccesEnginId] FOREIGN KEY ([DemandeAccesEnginId]) REFERENCES [dbo].[DemandeAccesEngin] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_CreatedBy]
    ON [dbo].[DemandeResultatEntete]([CreatedBy] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_DemandeAccesEnginId]
    ON [dbo].[DemandeResultatEntete]([DemandeAccesEnginId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AppFileId]
    ON [dbo].[DemandeResultatEntete]([AppFileId] ASC);

