CREATE TABLE [AccesEngins].[MAP_InfoGenerale_TypeCheckList] (
    [InfoGeneraleId]  BIGINT NOT NULL,
    [TypeCheckListId] BIGINT NOT NULL,
    CONSTRAINT [PK_dbo.MAP_InfoGenerale_TypeCheckList] PRIMARY KEY CLUSTERED ([InfoGeneraleId] ASC, [TypeCheckListId] ASC),
    CONSTRAINT [FK_dbo.MAP_InfoGenerale_TypeCheckList_AccesEngins.InfoGenerale_InfoGeneraleId] FOREIGN KEY ([InfoGeneraleId]) REFERENCES [AccesEngins].[REF_InfoGenerale] ([Id]),
    CONSTRAINT [FK_dbo.MAP_InfoGenerale_TypeCheckList_AccesEngins.TypeCheckList_TypeCheckListId] FOREIGN KEY ([TypeCheckListId]) REFERENCES [AccesEngins].[REF_TypeCheckList] ([Id])
);

