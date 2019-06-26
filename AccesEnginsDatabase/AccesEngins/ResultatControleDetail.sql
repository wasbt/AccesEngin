CREATE TABLE [AccesEngins].[ResultatControleDetail] (
    [Id]                      BIGINT         IDENTITY (1, 1) NOT NULL,
    [ResultatControleEnteteId] BIGINT         NOT NULL,
    [CheckListExigenceId]     BIGINT         NOT NULL,
    [IsConform]               BIT            NOT NULL,
    [DateExpiration]          DATETIME       NULL,
    [Observation]             NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_AccesEngins.ResultatControleDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccesEngins.ResultatControleDetail_AccesEngins.CheckListExigence_CheckListExigenceId] FOREIGN KEY ([CheckListExigenceId]) REFERENCES [AccesEngins].[REF_CheckListExigence] ([Id]),
    CONSTRAINT [FK_AccesEngins.ResultatControleDetail_AccesEngins.ResultatControleEntete_ResultatControleEnteteId] FOREIGN KEY ([ResultatControleEnteteId]) REFERENCES [AccesEngins].[ResultatControleEntete] ([Id])
);

