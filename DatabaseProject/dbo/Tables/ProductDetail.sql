CREATE TABLE [dbo].[ProductDetail] (
    [ProductId]  INT NOT NULL,
    [LocationId] INT NOT NULL,
    [Count]      INT NOT NULL,
    CONSTRAINT [PK_ProductDetail] PRIMARY KEY CLUSTERED ([ProductId] ASC, [LocationId] ASC),
    CONSTRAINT [FK_ProductDetail_Location_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [dbo].[Location] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProductDetail_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ProductDetail_LocationId]
    ON [dbo].[ProductDetail]([LocationId] ASC);

