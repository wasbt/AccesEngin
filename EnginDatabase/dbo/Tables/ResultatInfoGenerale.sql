﻿CREATE TABLE [dbo].[ResultatInfoGenerale] (
    [Id]                  BIGINT         IDENTITY (1, 1) NOT NULL,
    [DemandeAccesEnginId] BIGINT         NOT NULL,
    [InfoGeneraleId]      BIGINT         NOT NULL,
    [CreatedOn]           DATETIME       NOT NULL,
    [ValueInfo]           NVARCHAR (MAX) DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_dbo.ResultatInfoGenerale] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.ResultatInfoGenerale_dbo.DemandeAccesEngin_DemandeAccesEnginId] FOREIGN KEY ([DemandeAccesEnginId]) REFERENCES [dbo].[DemandeAccesEngin] ([Id]),
    CONSTRAINT [FK_dbo.ResultatInfoGenerale_dbo.InfoGenerale_InfoGeneraleId] FOREIGN KEY ([InfoGeneraleId]) REFERENCES [dbo].[InfoGenerale] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_InfoGeneraleId]
    ON [dbo].[ResultatInfoGenerale]([InfoGeneraleId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_DemandeAccesEnginId]
    ON [dbo].[ResultatInfoGenerale]([DemandeAccesEnginId] ASC);

