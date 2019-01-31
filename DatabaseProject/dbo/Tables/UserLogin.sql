CREATE TABLE [dbo].[UserLogin] (
    [LoginProvider]       NVARCHAR (150) NOT NULL,
    [ProviderKey]         NVARCHAR (150) NOT NULL,
    [ProviderDisplayName] NVARCHAR (150) NULL,
    [UserId]              INT            NOT NULL,
    CONSTRAINT [PK_UserLogin] PRIMARY KEY CLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC),
    CONSTRAINT [FK_UserLogin_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE
);




GO


