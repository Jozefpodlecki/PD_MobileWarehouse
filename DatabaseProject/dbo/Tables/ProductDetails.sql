CREATE TABLE [dbo].[ProductDetails] (
    [ProductId]  INT NOT NULL,
    [StateId]    INT NOT NULL,
    [LocationId] INT NOT NULL,
    [Count]      INT NOT NULL,
    CONSTRAINT [PK_ProductDetails] PRIMARY KEY CLUSTERED ([ProductId] ASC, [StateId] ASC, [LocationId] ASC),
    CONSTRAINT [FK_ProductDetails_Locations_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [dbo].[Locations] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProductDetails_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProductDetails_States_StateId] FOREIGN KEY ([StateId]) REFERENCES [dbo].[States] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ProductDetails_StateId]
    ON [dbo].[ProductDetails]([StateId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ProductDetails_LocationId]
    ON [dbo].[ProductDetails]([LocationId] ASC);

