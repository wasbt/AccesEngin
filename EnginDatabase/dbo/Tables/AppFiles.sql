CREATE TABLE [dbo].[AppFiles] (
    [Id]               BIGINT         IDENTITY (1, 1) NOT NULL,
    [SourceName]       NVARCHAR (MAX) NULL,
    [SourceId]         NVARCHAR (MAX) NULL,
    [ContainerName]    NVARCHAR (MAX) NULL,
    [OriginalFileName] NVARCHAR (MAX) NULL,
    [SystemFileName]   NVARCHAR (MAX) NULL,
    [FileSize]         BIGINT         NOT NULL,
    [CreatedOn]        DATETIME       NOT NULL,
    CONSTRAINT [PK_dbo.AppFiles] PRIMARY KEY CLUSTERED ([Id] ASC)
);

