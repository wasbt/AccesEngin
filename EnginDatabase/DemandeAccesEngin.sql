CREATE TABLE [dbo].[DemandeAccesEngin] (
    [Id]                 BIGINT         IDENTITY (1, 1) NOT NULL,
    [TypeCheckListId]    INT            NOT NULL,
    [Observation]        NVARCHAR (MAX) NULL,
    [CreatedOn]          DATETIME       NOT NULL,
    [CreatedBy]          NVARCHAR (128) NOT NULL,
    [TypeEnginId]        BIGINT         NOT NULL,
    [EntityId]           BIGINT         DEFAULT ((0)) NOT NULL,
    [Autorise]           BIT            NULL,
    [DatePlannification] DATETIME       DEFAULT ('1900-01-01T00:00:00.000') NOT NULL,
    CONSTRAINT [PK_dbo.DemandeAccesEngin] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.DemandeAccesEngin_dbo.AspNetUsers_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_dbo.DemandeAccesEngin_dbo.Entities_EntityId] FOREIGN KEY ([EntityId]) REFERENCES [dbo].[Entities] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.DemandeAccesEngin_dbo.TypeCheckList_TypeCheckListId] FOREIGN KEY ([TypeCheckListId]) REFERENCES [dbo].[TypeCheckList] ([Id]),
    CONSTRAINT [FK_dbo.DemandeAccesEngin_dbo.TypeEngin_TypeEngin_Id] FOREIGN KEY ([TypeEnginId]) REFERENCES [dbo].[TypeEngin] ([Id])
);










GO
CREATE NONCLUSTERED INDEX [IX_TypeCheckListId]
    ON [dbo].[DemandeAccesEngin]([TypeCheckListId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CreatedBy]
    ON [dbo].[DemandeAccesEngin]([CreatedBy] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TypeEnginId]
    ON [dbo].[DemandeAccesEngin]([TypeEnginId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_EntityId]
    ON [dbo].[DemandeAccesEngin]([EntityId] ASC);

