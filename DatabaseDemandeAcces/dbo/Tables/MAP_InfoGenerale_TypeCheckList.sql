CREATE TABLE [dbo].[MAP_InfoGenerale_TypeCheckList] (
    [InfoGeneraleId]  BIGINT NOT NULL,
    [TypeCheckListId] BIGINT NOT NULL,
    CONSTRAINT [PK_dbo.MAP_InfoGenerale_TypeCheckList] PRIMARY KEY CLUSTERED ([InfoGeneraleId] ASC, [TypeCheckListId] ASC),
    CONSTRAINT [FK_dbo.MAP_InfoGenerale_TypeCheckList_dbo.InfoGenerale_InfoGeneraleId] FOREIGN KEY ([InfoGeneraleId]) REFERENCES [dbo].[REF_InfoGenerale] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.MAP_InfoGenerale_TypeCheckList_dbo.TypeCheckList_TypeCheckListId] FOREIGN KEY ([TypeCheckListId]) REFERENCES [dbo].[REF_TypeCheckList] ([Id]) ON DELETE CASCADE
);

