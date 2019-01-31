CREATE TABLE [dbo].[Invoice] (
    [CreatedAt]        DATETIME2 (0) DEFAULT (getutcdate()) NOT NULL,
    [CreatedById]      INT           NULL,
    [LastModifiedAt]   DATETIME2 (0) DEFAULT (getutcdate()) NOT NULL,
    [LastModifiedById] INT           NULL,
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [CounterpartyId]   INT           NOT NULL,
    [IssueDate]        DATETIME2 (0) NOT NULL,
    [CompletionDate]   DATETIME2 (0) NOT NULL,
    [DocumentId]       NVARCHAR (50) NOT NULL,
    [Total]            MONEY         NOT NULL,
    [VAT]              MONEY         NOT NULL,
    [CityId]           INT           NOT NULL,
    [PaymentMethod]    TINYINT       NOT NULL,
    [InvoiceType]      TINYINT       NOT NULL,
    [CanEdit]          BIT           NOT NULL,
    CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Invoice_City_CityId] FOREIGN KEY ([CityId]) REFERENCES [dbo].[City] ([Id]),
    CONSTRAINT [FK_Invoice_Counterparties_CounterpartyId] FOREIGN KEY ([CounterpartyId]) REFERENCES [dbo].[Counterparties] ([Id]),
    CONSTRAINT [FK_Invoice_User_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_Invoice_User_LastModifiedById] FOREIGN KEY ([LastModifiedById]) REFERENCES [dbo].[User] ([Id])
);




GO



GO



GO



GO



GO


