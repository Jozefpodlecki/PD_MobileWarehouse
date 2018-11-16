CREATE TABLE [dbo].[History] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [UserId]    INT           NOT NULL,
    [CreatedAt] DATETIME2 (7) DEFAULT (getdate()) NOT NULL,
    [EventId]   INT           NOT NULL,
    CONSTRAINT [PK_History] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_History_Events_EventId] FOREIGN KEY ([EventId]) REFERENCES [dbo].[Events] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_History_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_History_UserId]
    ON [dbo].[History]([UserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_History_EventId]
    ON [dbo].[History]([EventId] ASC);

