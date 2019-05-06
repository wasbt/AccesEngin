CREATE TABLE [dbo].[REF_MailingList] (
    [Id]       NVARCHAR (128) NOT NULL,
    [EntityId] BIGINT         NOT NULL,
    [Name]     NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_dbo.REF_MailingList] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.REF_MailingList_dbo.Entities_EntityId] FOREIGN KEY ([EntityId]) REFERENCES [dbo].[Entities] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_EntityId]
    ON [dbo].[REF_MailingList]([EntityId] ASC);

