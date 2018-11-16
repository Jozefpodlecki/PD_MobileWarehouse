CREATE TABLE [dbo].[Attributes] (
    [Id]                   INT            IDENTITY (1, 1) NOT NULL,
    [Name]                 NVARCHAR (256) NULL,
    [IsDeleted]            BIT            DEFAULT ((0)) NOT NULL,
    [Order]                INT            NOT NULL,
    [LastModification]     DATETIME2 (7)  NOT NULL,
    [LastModificationById] INT            NOT NULL,
    [CreatedAt]            DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [CreatedById]          INT            NOT NULL,
    CONSTRAINT [PK_Attributes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Attribute_User] FOREIGN KEY ([LastModificationById]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_Attributes_User_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Attributes_LastModificationById]
    ON [dbo].[Attributes]([LastModificationById] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Attributes_CreatedById]
    ON [dbo].[Attributes]([CreatedById] ASC);

