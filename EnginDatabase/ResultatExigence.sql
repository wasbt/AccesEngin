CREATE TABLE [dbo].[ResultatExigence] (
    [Id]                  BIGINT         IDENTITY (1, 1) NOT NULL,
    [DemandeAccesEnginId] BIGINT         NOT NULL,
    [CheckListExigenceId] BIGINT         NOT NULL,
    [IsConform]           BIT            NOT NULL,
    [Date]                DATETIME       NULL,
    [CreatedOn]           DATETIME       NOT NULL,
    [Observation]         NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.ResultatExigence] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.ResultatExigence_dbo.CheckListExigence_CheckListExigenceId] FOREIGN KEY ([CheckListExigenceId]) REFERENCES [dbo].[CheckListExigence] ([Id]),
    CONSTRAINT [FK_dbo.ResultatExigence_dbo.DemandeAccesEngin_DemandeAccesEnginId] FOREIGN KEY ([DemandeAccesEnginId]) REFERENCES [dbo].[DemandeAccesEngin] ([Id])
);





GO
CREATE NONCLUSTERED INDEX [IX_DemandeAccesEnginId]
    ON [dbo].[ResultatExigence]([DemandeAccesEnginId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CheckListExigenceId]
    ON [dbo].[ResultatExigence]([CheckListExigenceId] ASC);

