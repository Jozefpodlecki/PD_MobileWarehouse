CREATE TABLE [dbo].[Entries] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (256) NOT NULL,
    [Count]     INT            NOT NULL,
    [VAT]       MONEY          NOT NULL,
    [Price]     MONEY          NOT NULL,
    [InvoiceId] INT            NOT NULL,
    CONSTRAINT [PK_Entries] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Entries_Invoice_InvoiceId] FOREIGN KEY ([InvoiceId]) REFERENCES [dbo].[Invoice] ([Id]) ON DELETE CASCADE
);




GO


