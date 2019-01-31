CREATE TABLE [dbo].[UserToken] (
    [UserId]        INT             NOT NULL,
    [LoginProvider] NVARCHAR (150)  NOT NULL,
    [Name]          NVARCHAR (150)  NOT NULL,
    [Value]         NVARCHAR (1000) NULL,
    CONSTRAINT [PK_UserToken] PRIMARY KEY CLUSTERED ([UserId] ASC, [LoginProvider] ASC, [Name] ASC),
    CONSTRAINT [FK_UserToken_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE
);



