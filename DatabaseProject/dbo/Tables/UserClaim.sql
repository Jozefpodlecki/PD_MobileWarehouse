CREATE TABLE [dbo].[UserClaim] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [UserId]     INT            NOT NULL,
    [ClaimType]  NVARCHAR (100) NOT NULL,
    [ClaimValue] NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_UserClaim] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserClaim_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [AK_UserClaim_Id_ClaimType_ClaimValue] UNIQUE NONCLUSTERED ([Id] ASC, [ClaimType] ASC, [ClaimValue] ASC)
);






GO


