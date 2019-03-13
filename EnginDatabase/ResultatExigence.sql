CREATE TABLE [dbo].[ResultatExigence] (
    [Id]                      BIGINT         IDENTITY (1, 1) NOT NULL,
    [CheckListExigenceId]     BIGINT         NOT NULL,
    [IsConform]               BIT            NOT NULL,
    [Date]                    DATETIME       NULL,
    [Observation]             NVARCHAR (MAX) NULL,
    [DemandeResultatEnteteId] BIGINT         DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_dbo.ResultatExigence] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.ResultatExigence_dbo.CheckListExigence_CheckListExigenceId] FOREIGN KEY ([CheckListExigenceId]) REFERENCES [dbo].[CheckListExigence] ([Id]),
    CONSTRAINT [FK_dbo.ResultatExigence_dbo.DemandeResultatEntete_DemandeResultatEnteteId] FOREIGN KEY ([DemandeResultatEnteteId]) REFERENCES [dbo].[DemandeResultatEntete] ([Id])
);







GO



GO
CREATE NONCLUSTERED INDEX [IX_CheckListExigenceId]
    ON [dbo].[ResultatExigence]([CheckListExigenceId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_DemandeResultatEnteteId]
    ON [dbo].[ResultatExigence]([DemandeResultatEnteteId] ASC);

