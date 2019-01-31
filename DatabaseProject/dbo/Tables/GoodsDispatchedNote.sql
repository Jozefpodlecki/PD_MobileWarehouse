CREATE TABLE [dbo].[GoodsDispatchedNote] (
    [IssueDate]        DATETIME2 (0)  NOT NULL,
    [Remarks]          NVARCHAR (MAX) NULL,
    [DocumentId]       NVARCHAR (50)  NOT NULL,
    [InvoiceId]        INT            NOT NULL,
    [DispatchDate]     DATETIME2 (0)  NOT NULL,
    [CreatedAt]        DATETIME2 (0)  DEFAULT (getutcdate()) NOT NULL,
    [CreatedById]      INT            NULL,
    [LastModifiedAt]   DATETIME2 (0)  DEFAULT (getutcdate()) NOT NULL,
    [LastModifiedById] INT            NULL,
    CONSTRAINT [PK_GoodsDispatchedNote] PRIMARY KEY CLUSTERED ([InvoiceId] ASC),
    CONSTRAINT [FK_GoodsDispatchedNote_Invoice_InvoiceId] FOREIGN KEY ([InvoiceId]) REFERENCES [dbo].[Invoice] ([Id]),
    CONSTRAINT [FK_GoodsDispatchedNote_User_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_GoodsDispatchedNote_User_LastModifiedById] FOREIGN KEY ([LastModifiedById]) REFERENCES [dbo].[User] ([Id])
);




GO



GO



GO


