CREATE TABLE [dbo].[InfoGenerale] (
    [Id]                    BIGINT         IDENTITY (1, 1) NOT NULL,
    [InfoGeneralRubriqueId] BIGINT         NOT NULL,
    [Name]                  NVARCHAR (MAX) NOT NULL,
    [CreatedOn]             DATETIME       NOT NULL,
    [CreatedBy]             NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.InfoGenerale] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.InfoGenerale_dbo.AspNetUsers_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_dbo.InfoGenerale_dbo.InfoGeneralRubrique_InfoGeneralRubriqueId] FOREIGN KEY ([InfoGeneralRubriqueId]) REFERENCES [dbo].[InfoGeneralRubrique] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_CreatedBy]
    ON [dbo].[InfoGenerale]([CreatedBy] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_InfoGeneralRubriqueId]
    ON [dbo].[InfoGenerale]([InfoGeneralRubriqueId] ASC);

