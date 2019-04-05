CREATE TABLE [dbo].[Reports] (
    [Id]                  BIGINT         IDENTITY (1, 1) NOT NULL,
    [DemandeAccesEnginId] BIGINT         NOT NULL,
    [MotifReport]         NVARCHAR (MAX) NULL,
    [CreatedBy]           NVARCHAR (MAX) NULL,
    [CreatedOn]           DATETIME       DEFAULT ('1900-01-01T00:00:00.000') NOT NULL,
    CONSTRAINT [PK_dbo.Reports] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Reports_dbo.DemandeAccesEngin_DemandeAccesEnginId] FOREIGN KEY ([DemandeAccesEnginId]) REFERENCES [dbo].[DemandeAccesEngin] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_DemandeAccesEnginId]
    ON [dbo].[Reports]([DemandeAccesEnginId] ASC);

