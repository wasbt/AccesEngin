CREATE TABLE [dbo].[DemandeAccesEngin] (
    [Id]                 BIGINT         IDENTITY (1, 1) NOT NULL,
    [TypeCheckListId]    BIGINT         NOT NULL,
    [Observation]        NVARCHAR (MAX) NULL,
    [CreatedOn]          DATETIME       NOT NULL,
    [CreatedBy]          NVARCHAR (128) NOT NULL,
    [TypeEnginId]        BIGINT         NOT NULL,
    [EntityId]           BIGINT         DEFAULT ((0)) NOT NULL,
    [Autorise]           BIT            NOT NULL,
    [DatePlannification] DATETIME       NOT NULL,
    [NatureMatiereId]    BIGINT         NULL,
    [AppFileId]          BIGINT         NULL,
    [StatutDemandeId]    BIGINT         NULL,
    [DateSortie]         DATETIME       NULL,
    CONSTRAINT [PK_dbo.DemandeAccesEngin] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.DemandeAccesEngin_dbo.AppFiles_AppFileId] FOREIGN KEY ([AppFileId]) REFERENCES [dbo].[AppFiles] ([Id]),
    CONSTRAINT [FK_dbo.DemandeAccesEngin_dbo.AspNetUsers_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_dbo.DemandeAccesEngin_dbo.Entities_EntityId] FOREIGN KEY ([EntityId]) REFERENCES [dbo].[Entities] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.DemandeAccesEngin_dbo.NatureMatiere_NatureMatiereId] FOREIGN KEY ([NatureMatiereId]) REFERENCES [dbo].[REF_NatureMatiere] ([Id]),
    CONSTRAINT [FK_dbo.DemandeAccesEngin_dbo.StatutDemandes_StatutDemandeId] FOREIGN KEY ([StatutDemandeId]) REFERENCES [dbo].[REF_StatutDemandes] ([Id]),
    CONSTRAINT [FK_dbo.DemandeAccesEngin_dbo.TypeCheckList_TypeCheckListId] FOREIGN KEY ([TypeCheckListId]) REFERENCES [dbo].[REF_TypeCheckList] ([Id]),
    CONSTRAINT [FK_dbo.DemandeAccesEngin_dbo.TypeEngin_TypeEngin_Id] FOREIGN KEY ([TypeEnginId]) REFERENCES [dbo].[REF_TypeEngin] ([Id])
);






















GO



GO



GO



GO



GO



GO



GO


