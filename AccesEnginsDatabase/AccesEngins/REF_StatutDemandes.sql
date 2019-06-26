CREATE TABLE [AccesEngins].[REF_StatutDemandes] (
    [Id]    BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]  NVARCHAR (MAX) NULL,
    [Color] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_AccesEngins.REF_StatutDemandes] PRIMARY KEY CLUSTERED ([Id] ASC)
);