CREATE TABLE [dbo].[User] (
    [Id]                   INT                IDENTITY (1, 1) NOT NULL,
    [UserName]             NVARCHAR (64)      NULL,
    [NormalizedUserName]   NVARCHAR (256)     NULL,
    [Email]                NVARCHAR (64)      NULL,
    [NormalizedEmail]      NVARCHAR (256)     NULL,
    [EmailConfirmed]       BIT                NOT NULL,
    [SecurityStamp]        NVARCHAR (255)     NULL,
    [ConcurrencyStamp]     NVARCHAR (64)      NULL,
    [PhoneNumber]          NVARCHAR (64)      NULL,
    [PhoneNumberConfirmed] BIT                NOT NULL,
    [TwoFactorEnabled]     BIT                NOT NULL,
    [LockoutEnd]           DATETIMEOFFSET (7) NULL,
    [LockoutEnabled]       BIT                NOT NULL,
    [AccessFailedCount]    INT                NOT NULL,
    [FirstName]            NVARCHAR (64)      NULL,
    [LastName]             NVARCHAR (64)      NULL,
    [Image]                NVARCHAR (MAX)     NULL,
    [UserStatus]           TINYINT            NOT NULL,
    [PasswordHash]         VARBINARY (64)     NULL,
    [LastLogin]            DATETIME2 (7)      NULL,
    [CreatedAt]            DATETIME2 (0)      DEFAULT (getutcdate()) NOT NULL,
    [CreatedById]          INT                NULL,
    [LastModifiedAt]       DATETIME2 (0)      DEFAULT (getutcdate()) NOT NULL,
    [LastModifiedById]     INT                NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_User_User_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[User] ([Id])
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex]
    ON [dbo].[User]([NormalizedUserName] ASC) WHERE ([NormalizedUserName] IS NOT NULL);


GO
CREATE NONCLUSTERED INDEX [EmailIndex]
    ON [dbo].[User]([NormalizedEmail] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_User_Email]
    ON [dbo].[User]([Email] ASC) WHERE ([Email] IS NOT NULL);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_User_CreatedById]
    ON [dbo].[User]([CreatedById] ASC) WHERE ([CreatedById] IS NOT NULL);

