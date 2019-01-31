CREATE TABLE [dbo].[ProductDetail] (
    [ProductId]        INT           NOT NULL,
    [LocationId]       INT           NOT NULL,
    [Count]            INT           NOT NULL,
    [CreatedAt]        DATETIME2 (7) NULL,
    [CreatedById]      INT           NULL,
    [LastModifiedAt]   DATETIME2 (7) NULL,
    [LastModifiedById] INT           NULL,
    CONSTRAINT [PK_ProductDetail] PRIMARY KEY CLUSTERED ([ProductId] ASC, [LocationId] ASC),
    CONSTRAINT [FK_ProductDetail_Location_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [dbo].[Location] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProductDetail_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProductDetail_User_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_ProductDetail_User_LastModifiedById] FOREIGN KEY ([LastModifiedById]) REFERENCES [dbo].[User] ([Id])
);




GO


