CREATE TABLE [dbo].[Products] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [Name]               NVARCHAR (256) NULL,
    [Image]              NVARCHAR (MAX) NULL,
    [IsDeleted]          BIT            DEFAULT ((0)) NOT NULL,
    [LastModification]   DATETIME2 (7)  NOT NULL,
    [LastModificationBy] INT            NOT NULL,
    [CreatedAt]          DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [CreatedById]        INT            NOT NULL,
    [Count]              INT            NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Products_User_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Products_CreatedById]
    ON [dbo].[Products]([CreatedById] ASC);

