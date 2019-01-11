CREATE TABLE [dbo].[GoodsReceivedNote] (
    [IssueDate]        DATETIME2 (0)  NOT NULL,
    [Remarks]          NVARCHAR (MAX) NULL,
    [DocumentId]       NVARCHAR (50)  NOT NULL,
    [InvoiceId]        INT            NOT NULL,
    [ReceiveDate]      DATETIME2 (0)  NOT NULL,
    [CreatedAt]        DATETIME2 (0)  DEFAULT (getutcdate()) NOT NULL,
    [CreatedById]      INT            NULL,
    [LastModifiedAt]   DATETIME2 (0)  DEFAULT (getutcdate()) NOT NULL,
    [LastModifiedById] INT            NULL,
    CONSTRAINT [PK_GoodsReceivedNote] PRIMARY KEY CLUSTERED ([InvoiceId] ASC),
    CONSTRAINT [FK_GoodsReceivedNote_Invoice_InvoiceId] FOREIGN KEY ([InvoiceId]) REFERENCES [dbo].[Invoice] ([Id]),
    CONSTRAINT [FK_GoodsReceivedNote_User_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_GoodsReceivedNote_User_LastModifiedById] FOREIGN KEY ([LastModifiedById]) REFERENCES [dbo].[User] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_GoodsReceivedNote_LastModifiedById]
    ON [dbo].[GoodsReceivedNote]([LastModifiedById] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_GoodsReceivedNote_DocumentId]
    ON [dbo].[GoodsReceivedNote]([DocumentId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_GoodsReceivedNote_CreatedById]
    ON [dbo].[GoodsReceivedNote]([CreatedById] ASC);

