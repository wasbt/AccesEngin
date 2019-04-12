CREATE TABLE [dbo].[Map_REF_MailingListAspNetUsers] (
    [MailingListId] NVARCHAR (128) NOT NULL,
    [UserId]        NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.Map_REF_MailingListAspNetUsers] PRIMARY KEY CLUSTERED ([MailingListId] ASC, [UserId] ASC),
    CONSTRAINT [FK_dbo.Map_REF_MailingListAspNetUsers_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.Map_REF_MailingListAspNetUsers_dbo.REF_MailingList_MailingListId] FOREIGN KEY ([MailingListId]) REFERENCES [dbo].[REF_MailingList] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[Map_REF_MailingListAspNetUsers]([UserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_MailingListId]
    ON [dbo].[Map_REF_MailingListAspNetUsers]([MailingListId] ASC);

