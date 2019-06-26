CREATE TABLE [AccesEngins].[DemandeAccesEnginInfoGeneraleValue] (
    [Id]                  BIGINT         IDENTITY (1, 1) NOT NULL,
    [DemandeAccesEnginId] BIGINT         NOT NULL,
    [InfoGeneraleId]      BIGINT         NOT NULL,
    [CreatedOn]           DATETIME       NOT NULL,
    [ValueInfo]           NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_AccesEngins.DemandeAccesEnginInfoGeneraleValue] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccesEngins.DemandeAccesEnginInfoGeneraleValue_AccesEngins.DemandeAccesEngin_DemandeAccesEnginId] FOREIGN KEY ([DemandeAccesEnginId]) REFERENCES [AccesEngins].[DemandeAccesEngin] ([Id]),
    CONSTRAINT [FK_AccesEngins.DemandeAccesEnginInfoGeneraleValue_AccesEngins.InfoGenerale_InfoGeneraleId] FOREIGN KEY ([InfoGeneraleId]) REFERENCES [AccesEngins].[REF_InfoGenerale] ([Id])
);