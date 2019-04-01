CREATE TABLE [dbo].[Reports] (
    [Id]                  BIGINT         IDENTITY (1, 1) NOT NULL,
    [DemandeAccesEnginId] BIGINT         NOT NULL,
    [AncienneDate]        DATETIME       NOT NULL,
    [NouvelleDate]        DATETIME       NOT NULL,
    [MotifReport]         NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.Reports] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Reports_dbo.DemandeAccesEngin_DemandeAccesEnginId] FOREIGN KEY ([DemandeAccesEnginId]) REFERENCES [dbo].[DemandeAccesEngin] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_DemandeAccesEnginId]
    ON [dbo].[Reports]([DemandeAccesEnginId] ASC);

