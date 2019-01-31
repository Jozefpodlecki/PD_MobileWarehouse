CREATE TABLE [dbo].[Role] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (256) NULL,
    [NormalizedName]   NVARCHAR (256) NULL,
    [ConcurrencyStamp] NVARCHAR (MAX) NULL,
    [CreatedAt]        DATETIME2 (0)  DEFAULT (getutcdate()) NOT NULL,
    [CreatedById]      INT            NULL,
    [LastModifiedAt]   DATETIME2 (0)  DEFAULT (getutcdate()) NOT NULL,
    [LastModifiedById] INT            NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Role_User_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_Role_User_LastModifiedById] FOREIGN KEY ([LastModifiedById]) REFERENCES [dbo].[User] ([Id])
);






GO



GO



GO



GO


