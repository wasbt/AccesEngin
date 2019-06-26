CREATE TABLE [AccesEngins].[ReponseDemande] (
    [Id]                  BIGINT         IDENTITY (1, 1) NOT NULL,
    [DemandeAccesEnginId] BIGINT         NOT NULL,
    [Motif]               NVARCHAR (MAX) NULL,
    [CreatedBy]           NVARCHAR (MAX) NULL,
    [CreatedOn]           DATETIME       NOT NULL,
    CONSTRAINT [PK_AccesEngins.ReponseDemande] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccesEngins.ReponseDemande_dbo.DemandeAccesEngin_DemandeAccesEnginId] FOREIGN KEY ([DemandeAccesEnginId]) REFERENCES [AccesEngins].[DemandeAccesEngin] ([Id])
);

