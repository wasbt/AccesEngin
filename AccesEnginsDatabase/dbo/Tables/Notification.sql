CREATE TABLE [dbo].[Notification] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Content]      NVARCHAR (MAX) NOT NULL,
    [ObjectType]   NVARCHAR (MAX) NULL,
    [ObjectId]     INT            NULL,
    [UserId]       NVARCHAR (128) NOT NULL,
    [DtNotif]      DATETIME       NOT NULL,
    [SenderUserId] NVARCHAR (128) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Notification_ToUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);
