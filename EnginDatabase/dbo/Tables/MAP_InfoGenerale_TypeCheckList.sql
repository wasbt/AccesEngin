CREATE TABLE [dbo].[MAP_InfoGenerale_TypeCheckList] (
    [InfoGeneraleId]  BIGINT NOT NULL,
    [TypeCheckListId] INT    NOT NULL,
    CONSTRAINT [PK_dbo.MAP_InfoGenerale_TypeCheckList] PRIMARY KEY CLUSTERED ([InfoGeneraleId] ASC, [TypeCheckListId] ASC),
    CONSTRAINT [FK_dbo.MAP_InfoGenerale_TypeCheckList_dbo.InfoGenerale_InfoGeneraleId] FOREIGN KEY ([InfoGeneraleId]) REFERENCES [dbo].[InfoGenerale] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.MAP_InfoGenerale_TypeCheckList_dbo.TypeCheckList_TypeCheckListId] FOREIGN KEY ([TypeCheckListId]) REFERENCES [dbo].[TypeCheckList] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_TypeCheckListId]
    ON [dbo].[MAP_InfoGenerale_TypeCheckList]([TypeCheckListId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_InfoGeneraleId]
    ON [dbo].[MAP_InfoGenerale_TypeCheckList]([InfoGeneraleId] ASC);

