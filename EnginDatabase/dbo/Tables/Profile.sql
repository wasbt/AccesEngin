CREATE TABLE [dbo].[Profile] (
    [Id]               NVARCHAR (128) NOT NULL,
    [FullName]         NVARCHAR (MAX) NULL,
    [Email]            NVARCHAR (MAX) NULL,
    [Phone]            NVARCHAR (MAX) NULL,
    [Details]          NVARCHAR (MAX) NULL,
    [DtLastConnection] DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_UserProfile_ToAspNetUsers] FOREIGN KEY ([Id]) REFERENCES [dbo].[AspNetUsers]([Id])

);

