CREATE TABLE [dbo].[City] (
    [CreatedAt]        DATETIME2 (0) DEFAULT (getutcdate()) NOT NULL,
    [CreatedById]      INT           NULL,
    [LastModifiedAt]   DATETIME2 (0) DEFAULT (getutcdate()) NOT NULL,
    [LastModifiedById] INT           NULL,
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_City_User_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_City_User_LastModifiedById] FOREIGN KEY ([LastModifiedById]) REFERENCES [dbo].[User] ([Id])
);




GO



GO



GO


