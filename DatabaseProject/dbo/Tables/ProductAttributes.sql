CREATE TABLE [dbo].[ProductAttributes] (
    [ProductId]     INT            NOT NULL,
    [AttributeId]   INT            NOT NULL,
    [IntValue]      INT            NOT NULL,
    [StringValue]   NVARCHAR (MAX) NULL,
    [BoolValue]     BIT            NOT NULL,
    [DateTimeValue] DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_ProductAttributes] PRIMARY KEY CLUSTERED ([ProductId] ASC, [AttributeId] ASC),
    CONSTRAINT [FK_ProductAttributes_Attributes_AttributeId] FOREIGN KEY ([AttributeId]) REFERENCES [dbo].[Attributes] ([Id]),
    CONSTRAINT [FK_ProductAttributes_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ProductAttributes_AttributeId]
    ON [dbo].[ProductAttributes]([AttributeId] ASC);

