﻿CREATE TABLE [AccesEngins].[DemandeAccesEngin] (
    [Id]                 BIGINT          IDENTITY (1, 1) NOT NULL,
    [EntiteId]           INT             NOT NULL,
    [TypeCheckListId]    BIGINT          NOT NULL,
    [Observation]        NVARCHAR (MAX)  NULL,
    [TypeEnginId]        BIGINT          NOT NULL,
    [DatePlannification] DATETIME        NOT NULL,
    [NatureMatiereId]    BIGINT          NULL,
    [AppFileId]          BIGINT          NULL,
    [StatutDemandeId]    BIGINT          NULL,
    [DateSortie]         DATETIME        NULL,
    [CreatedOn]          DATETIME        NOT NULL,
    [CreatedBy]          NVARCHAR (128)  NOT NULL,
    [Data]               VARBINARY (MAX) NULL,
    CONSTRAINT [PK_AccesEngins.DemandeAccesEngin] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccesEngins.DemandeAccesEngin_AccesEngins.NatureMatiere_NatureMatiereId] FOREIGN KEY ([NatureMatiereId]) REFERENCES [AccesEngins].[REF_NatureMatiere] ([Id]),
    CONSTRAINT [FK_AccesEngins.DemandeAccesEngin_AccesEngins.StatutDemandes_StatutDemandeId] FOREIGN KEY ([StatutDemandeId]) REFERENCES [AccesEngins].[REF_StatutDemandes] ([Id]),
    CONSTRAINT [FK_AccesEngins.DemandeAccesEngin_AccesEngins.TypeCheckList_TypeCheckListId] FOREIGN KEY ([TypeCheckListId]) REFERENCES [AccesEngins].[REF_TypeCheckList] ([Id]),
    CONSTRAINT [FK_AccesEngins.DemandeAccesEngin_AccesEngins.TypeEngin_TypeEngin_Id] FOREIGN KEY ([TypeEnginId]) REFERENCES [AccesEngins].[REF_TypeEngin] ([Id]),
    CONSTRAINT [FK_AccesEngins.DemandeAccesEngin_dbo.AppFiles_AppFileId] FOREIGN KEY ([AppFileId]) REFERENCES [dbo].[AppFile] ([AppFileId]),
    CONSTRAINT [FK_AccesEngins.DemandeAccesEngin_dbo.AspNetUsers_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_AccesEngins.DemandeAccesEngin_dbo.Entite_EntiteId] FOREIGN KEY ([EntiteId]) REFERENCES [dbo].[Entite] ([Id])
);



