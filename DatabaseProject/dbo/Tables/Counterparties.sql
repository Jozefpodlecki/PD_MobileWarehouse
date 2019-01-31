CREATE TABLE [dbo].[Counterparties] (
    [CreatedAt]        DATETIME2 (0) DEFAULT (getutcdate()) NOT NULL,
    [CreatedById]      INT           NULL,
    [LastModifiedAt]   DATETIME2 (0) DEFAULT (getutcdate()) NOT NULL,
    [LastModifiedById] INT           NULL,
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (50) NOT NULL,
    [PostalCode]       NVARCHAR (10) NULL,
    [Street]           NVARCHAR (50) NULL,
    [CityId]           INT           NOT NULL,
    [PhoneNumber]      NVARCHAR (20) NULL,
    [NIP]              NVARCHAR (20) NOT NULL,
    CONSTRAINT [PK_Counterparties] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Counterparties_City_CityId] FOREIGN KEY ([CityId]) REFERENCES [dbo].[City] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Counterparties_User_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_Counterparties_User_LastModifiedById] FOREIGN KEY ([LastModifiedById]) REFERENCES [dbo].[User] ([Id])
);




GO



GO



GO


