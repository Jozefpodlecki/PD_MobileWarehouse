CREATE TABLE [dbo].[Product] (
    [CreatedAt]        DATETIME2 (0)   DEFAULT (getutcdate()) NOT NULL,
    [CreatedById]      INT             NULL,
    [LastModifiedAt]   DATETIME2 (0)   DEFAULT (getutcdate()) NOT NULL,
    [LastModifiedById] INT             NULL,
    [Id]               INT             IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (100)  NOT NULL,
    [Image]            NVARCHAR (MAX)  NULL,
    [Price]            DECIMAL (18, 2) NOT NULL,
    [VAT]              DECIMAL (18, 2) NOT NULL,
    [Barcode]          NVARCHAR (MAX)  NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Product_User_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_Product_User_LastModifiedById] FOREIGN KEY ([LastModifiedById]) REFERENCES [dbo].[User] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Product_Name]
    ON [dbo].[Product]([Name] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Product_LastModifiedById]
    ON [dbo].[Product]([LastModifiedById] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Product_CreatedById]
    ON [dbo].[Product]([CreatedById] ASC);

