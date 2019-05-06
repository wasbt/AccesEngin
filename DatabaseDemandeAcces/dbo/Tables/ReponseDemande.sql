CREATE TABLE [dbo].[ReponseDemande] (
    [Id]                  BIGINT         IDENTITY (1, 1) NOT NULL,
    [DemandeAccesEnginId] BIGINT         NOT NULL,
    [Motif]               NVARCHAR (MAX) NULL,
    [CreatedBy]           NVARCHAR (MAX) NULL,
    [CreatedOn]           DATETIME       DEFAULT ('1900-01-01T00:00:00.000') NOT NULL,
    CONSTRAINT [PK_dbo.ReponseDemande] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Reports_dbo.DemandeAccesEngin_DemandeAccesEnginId] FOREIGN KEY ([DemandeAccesEnginId]) REFERENCES [dbo].[DemandeAccesEngin] ([Id])
);

