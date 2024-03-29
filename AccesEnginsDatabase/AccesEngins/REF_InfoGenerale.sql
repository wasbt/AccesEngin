﻿CREATE TABLE [AccesEngins].[REF_InfoGenerale] (
    [Id]                    BIGINT         IDENTITY (1, 1) NOT NULL,
    [InfoGeneralRubriqueId] BIGINT         NOT NULL,
    [Name]                  NVARCHAR (MAX) NOT NULL,
    [CreatedOn]             DATETIME       NOT NULL,
    [CreatedBy]             NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.REF_InfoGenerale] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.InfoGenerale_dbo.AspNetUsers_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_AccesEngins.InfoGenerale_AccesEngins.InfoGeneralRubrique_InfoGeneralRubriqueId] FOREIGN KEY ([InfoGeneralRubriqueId]) REFERENCES [AccesEngins].[REF_InfoGeneralRubrique] ([Id])
);

