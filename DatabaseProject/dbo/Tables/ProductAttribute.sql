CREATE TABLE [dbo].[ProductAttribute] (
    [ProductId]        INT            NOT NULL,
    [AttributeId]      INT            NOT NULL,
    [Value]            NVARCHAR (255) NOT NULL,
    [CreatedAt]        DATETIME2 (7)  NULL,
    [CreatedById]      INT            NULL,
    [LastModifiedAt]   DATETIME2 (7)  NULL,
    [LastModifiedById] INT            NULL,
    CONSTRAINT [PK_ProductAttribute] PRIMARY KEY CLUSTERED ([ProductId] ASC, [AttributeId] ASC),
    CONSTRAINT [FK_ProductAttribute_Attribute_AttributeId] FOREIGN KEY ([AttributeId]) REFERENCES [dbo].[Attribute] ([Id]),
    CONSTRAINT [FK_ProductAttribute_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProductAttribute_User_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_ProductAttribute_User_LastModifiedById] FOREIGN KEY ([LastModifiedById]) REFERENCES [dbo].[User] ([Id])
);




GO


