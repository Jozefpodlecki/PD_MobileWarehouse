CREATE TABLE [dbo].[RoleClaim] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [RoleId]     INT            NOT NULL,
    [ClaimType]  NVARCHAR (100) NOT NULL,
    [ClaimValue] NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_RoleClaim] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RoleClaim_Role_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [AK_RoleClaim_Id_ClaimType_ClaimValue] UNIQUE NONCLUSTERED ([Id] ASC, [ClaimType] ASC, [ClaimValue] ASC)
);






GO


