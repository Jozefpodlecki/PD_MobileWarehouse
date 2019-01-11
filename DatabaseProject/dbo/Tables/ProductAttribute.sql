CREATE TABLE [dbo].[ProductAttribute] (
    [ProductId]   INT            NOT NULL,
    [AttributeId] INT            NOT NULL,
    [Value]       NVARCHAR (255) NOT NULL,
    CONSTRAINT [PK_ProductAttribute] PRIMARY KEY CLUSTERED ([ProductId] ASC, [AttributeId] ASC),
    CONSTRAINT [FK_ProductAttribute_Attribute_AttributeId] FOREIGN KEY ([AttributeId]) REFERENCES [dbo].[Attribute] ([Id]),
    CONSTRAINT [FK_ProductAttribute_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ProductAttribute_AttributeId]
    ON [dbo].[ProductAttribute]([AttributeId] ASC);

