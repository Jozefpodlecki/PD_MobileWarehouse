
CREATE DATABASE [MobileWarehouseDatabase]
GO

CREATE LOGIN [MobileWarehouseSystemUser] WITH PASSWORD=N'OmR3jcVfxlLW74ywlrl5JX9uAdup4jU52HnDQfsmKe4=', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

ALTER LOGIN [MobileWarehouseSystemUser] ENABLE
GO

USE [MobileWarehouseDatabase]
GO

SET NOCOUNT ON
GO

CREATE USER [MobileWarehouseSystemUser] FOR LOGIN [MobileWarehouseSystemUser] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [MobileWarehouseSystemUser]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [MobileWarehouseSystemUser]
GO
ALTER ROLE [db_datareader] ADD MEMBER [MobileWarehouseSystemUser]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [MobileWarehouseSystemUser]
GO
/****** Object:  Table [dbo].[Attribute]    Script Date: 12.01.2019 23:21:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attribute](
	[CreatedAt] [datetime2](0) NOT NULL,
	[CreatedById] [int] NULL,
	[LastModifiedAt] [datetime2](0) NOT NULL,
	[LastModifiedById] [int] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Order] [int] NOT NULL,
 CONSTRAINT [PK_Attribute] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[City]    Script Date: 12.01.2019 23:21:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City](
	[CreatedAt] [datetime2](0) NOT NULL,
	[CreatedById] [int] NULL,
	[LastModifiedAt] [datetime2](0) NOT NULL,
	[LastModifiedById] [int] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Counterparties]    Script Date: 12.01.2019 23:21:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Counterparties](
	[CreatedAt] [datetime2](0) NOT NULL,
	[CreatedById] [int] NULL,
	[LastModifiedAt] [datetime2](0) NOT NULL,
	[LastModifiedById] [int] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[PostalCode] [nvarchar](10) NULL,
	[Street] [nvarchar](50) NULL,
	[CityId] [int] NOT NULL,
	[PhoneNumber] [nvarchar](20) NULL,
	[NIP] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Counterparties] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Entries]    Script Date: 12.01.2019 23:21:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Count] [int] NOT NULL,
	[VAT] [money] NOT NULL,
	[Price] [money] NOT NULL,
	[InvoiceId] [int] NOT NULL,
 CONSTRAINT [PK_Entries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GoodsDispatchedNote]    Script Date: 12.01.2019 23:21:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GoodsDispatchedNote](
	[IssueDate] [datetime2](0) NOT NULL,
	[Remarks] [nvarchar](max) NULL,
	[DocumentId] [nvarchar](50) NOT NULL,
	[InvoiceId] [int] NOT NULL,
	[DispatchDate] [datetime2](0) NOT NULL,
	[CreatedAt] [datetime2](0) NOT NULL,
	[CreatedById] [int] NULL,
	[LastModifiedAt] [datetime2](0) NOT NULL,
	[LastModifiedById] [int] NULL,
 CONSTRAINT [PK_GoodsDispatchedNote] PRIMARY KEY CLUSTERED 
(
	[InvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GoodsReceivedNote]    Script Date: 12.01.2019 23:21:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GoodsReceivedNote](
	[IssueDate] [datetime2](0) NOT NULL,
	[Remarks] [nvarchar](max) NULL,
	[DocumentId] [nvarchar](50) NOT NULL,
	[InvoiceId] [int] NOT NULL,
	[ReceiveDate] [datetime2](0) NOT NULL,
	[CreatedAt] [datetime2](0) NOT NULL,
	[CreatedById] [int] NULL,
	[LastModifiedAt] [datetime2](0) NOT NULL,
	[LastModifiedById] [int] NULL,
 CONSTRAINT [PK_GoodsReceivedNote] PRIMARY KEY CLUSTERED 
(
	[InvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 12.01.2019 23:21:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice](
	[CreatedAt] [datetime2](0) NOT NULL,
	[CreatedById] [int] NULL,
	[LastModifiedAt] [datetime2](0) NOT NULL,
	[LastModifiedById] [int] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CounterpartyId] [int] NOT NULL,
	[IssueDate] [datetime2](0) NOT NULL,
	[CompletionDate] [datetime2](0) NOT NULL,
	[DocumentId] [nvarchar](50) NOT NULL,
	[Total] [money] NOT NULL,
	[VAT] [money] NOT NULL,
	[CityId] [int] NOT NULL,
	[PaymentMethod] [tinyint] NOT NULL,
	[InvoiceType] [tinyint] NOT NULL,
	[CanEdit] [bit] NOT NULL,
 CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Location]    Script Date: 12.01.2019 23:21:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Location](
	[CreatedAt] [datetime2](0) NOT NULL,
	[CreatedById] [int] NULL,
	[LastModifiedAt] [datetime2](0) NOT NULL,
	[LastModifiedById] [int] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 12.01.2019 23:21:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[CreatedAt] [datetime2](0) NOT NULL,
	[CreatedById] [int] NULL,
	[LastModifiedAt] [datetime2](0) NOT NULL,
	[LastModifiedById] [int] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Image] [nvarchar](max) NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[VAT] [decimal](18, 2) NOT NULL,
	[Barcode] [nvarchar](max) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductAttribute]    Script Date: 12.01.2019 23:21:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductAttribute](
	[ProductId] [int] NOT NULL,
	[AttributeId] [int] NOT NULL,
	[Value] [nvarchar](255) NOT NULL,
	[CreatedAt] [datetime2](7) NULL,
	[CreatedById] [int] NULL,
	[LastModifiedAt] [datetime2](7) NULL,
	[LastModifiedById] [int] NULL,
 CONSTRAINT [PK_ProductAttribute] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC,
	[AttributeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductDetail]    Script Date: 12.01.2019 23:21:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductDetail](
	[ProductId] [int] NOT NULL,
	[LocationId] [int] NOT NULL,
	[Count] [int] NOT NULL,
	[CreatedAt] [datetime2](7) NULL,
	[CreatedById] [int] NULL,
	[LastModifiedAt] [datetime2](7) NULL,
	[LastModifiedById] [int] NULL,
 CONSTRAINT [PK_ProductDetail] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC,
	[LocationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 12.01.2019 23:21:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[CreatedAt] [datetime2](0) NOT NULL,
	[CreatedById] [int] NULL,
	[LastModifiedAt] [datetime2](0) NOT NULL,
	[LastModifiedById] [int] NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleClaim]    Script Date: 12.01.2019 23:21:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleClaim](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[ClaimType] [nvarchar](100) NOT NULL,
	[ClaimValue] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_RoleClaim] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 12.01.2019 23:21:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](64) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](64) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[SecurityStamp] [nvarchar](255) NULL,
	[ConcurrencyStamp] [nvarchar](64) NULL,
	[PhoneNumber] [nvarchar](64) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[FirstName] [nvarchar](64) NULL,
	[LastName] [nvarchar](64) NULL,
	[Image] [nvarchar](max) NULL,
	[UserStatus] [tinyint] NOT NULL,
	[PasswordHash] [varbinary](64) NULL,
	[LastLogin] [datetime2](7) NULL,
	[CreatedAt] [datetime2](0) NOT NULL,
	[CreatedById] [int] NULL,
	[LastModifiedAt] [datetime2](0) NOT NULL,
	[LastModifiedById] [int] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserClaim]    Script Date: 12.01.2019 23:21:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserClaim](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ClaimType] [nvarchar](100) NOT NULL,
	[ClaimValue] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_UserClaim] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLogin]    Script Date: 12.01.2019 23:21:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLogin](
	[LoginProvider] [nvarchar](150) NOT NULL,
	[ProviderKey] [nvarchar](150) NOT NULL,
	[ProviderDisplayName] [nvarchar](150) NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_UserLogin] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 12.01.2019 23:21:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserToken]    Script Date: 12.01.2019 23:21:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserToken](
	[UserId] [int] NOT NULL,
	[LoginProvider] [nvarchar](150) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Value] [nvarchar](1000) NULL,
 CONSTRAINT [PK_UserToken] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Role] ON 
GO
INSERT [dbo].[Role] ([Id], [Name], [NormalizedName], [ConcurrencyStamp], [CreatedAt], [CreatedById], [LastModifiedAt], [LastModifiedById]) VALUES (1, N'Pracownik', N'PRACOWNIK', N'9c9895dd-8a0a-405b-88ec-77f436c6b2c4', CAST(N'2019-01-10T19:33:06.0000000' AS DateTime2), NULL, CAST(N'2019-01-10T19:33:06.0000000' AS DateTime2), NULL)
GO
INSERT [dbo].[Role] ([Id], [Name], [NormalizedName], [ConcurrencyStamp], [CreatedAt], [CreatedById], [LastModifiedAt], [LastModifiedById]) VALUES (2, N'Ksiegowy', N'KSIEGOWY', N'c4a23e69-956a-4f40-bb1e-30277a425b1b', CAST(N'2019-01-10T19:33:07.0000000' AS DateTime2), NULL, CAST(N'2019-01-10T19:33:07.0000000' AS DateTime2), NULL)
GO
INSERT [dbo].[Role] ([Id], [Name], [NormalizedName], [ConcurrencyStamp], [CreatedAt], [CreatedById], [LastModifiedAt], [LastModifiedById]) VALUES (3, N'Kierownik', N'KIEROWNIK', N'bd3afa65-f22f-459b-9584-48b7ef5baa51', CAST(N'2019-01-10T19:33:07.0000000' AS DateTime2), NULL, CAST(N'2019-01-10T19:33:07.0000000' AS DateTime2), NULL)
GO
INSERT [dbo].[Role] ([Id], [Name], [NormalizedName], [ConcurrencyStamp], [CreatedAt], [CreatedById], [LastModifiedAt], [LastModifiedById]) VALUES (4, N'Administrator', N'ADMINISTRATOR', N'8c5efe50-73f5-4e0b-823d-626b24ff4ab1', CAST(N'2019-01-10T19:33:07.0000000' AS DateTime2), NULL, CAST(N'2019-01-10T19:33:07.0000000' AS DateTime2), NULL)
GO
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[RoleClaim] ON 
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (1, 1, N'Permission', N'Locations.Read')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (2, 1, N'Permission', N'Locations.Add')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (3, 1, N'Permission', N'Locations.Update')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (4, 1, N'Permission', N'Notes.Read')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (5, 1, N'Permission', N'Notes.Add')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (6, 1, N'Permission', N'Notes.Update')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (7, 1, N'Permission', N'Notes.Remove')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (8, 1, N'Permission', N'ScanBarcode')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (9, 1, N'Permission', N'ScanQRCode')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (10, 2, N'Permission', N'Invoices.Read')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (11, 2, N'Permission', N'Invoices.Add')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (12, 2, N'Permission', N'Invoices.Update')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (13, 2, N'Permission', N'Invoices.Remove')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (14, 2, N'Permission', N'Cities.Read')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (15, 2, N'Permission', N'Counterparties.Read')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (16, 3, N'Permission', N'SeeDetails')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (17, 3, N'Permission', N'ScanQRCode')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (18, 3, N'Permission', N'ScanBarcode')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (19, 3, N'Permission', N'Users.Remove')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (20, 3, N'Permission', N'Users.Update')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (21, 3, N'Permission', N'Users.Add')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (22, 3, N'Permission', N'Users.Read')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (23, 3, N'Permission', N'Invoices.Remove')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (24, 3, N'Permission', N'Invoices.Update')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (25, 3, N'Permission', N'Invoices.Add')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (26, 3, N'Permission', N'Invoices.Read')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (27, 3, N'Permission', N'Notes.Remove')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (28, 3, N'Permission', N'Notes.Update')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (29, 3, N'Permission', N'Notes.Add')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (30, 3, N'Permission', N'Notes.Read')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (31, 3, N'Permission', N'Locations.Update')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (32, 3, N'Permission', N'Locations.Add')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (33, 3, N'Permission', N'Locations.Read')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (34, 3, N'Permission', N'Cities.Read')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (35, 3, N'Permission', N'Counterparties.Read')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (36, 4, N'Permission', N'Users.Read')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (37, 4, N'Permission', N'ScanBarcode')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (38, 4, N'Permission', N'Locations.Remove')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (39, 4, N'Permission', N'Locations.Update')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (40, 4, N'Permission', N'Locations.Add')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (41, 4, N'Permission', N'Locations.Read')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (42, 4, N'Permission', N'Attributes.Remove')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (43, 4, N'Permission', N'Attributes.Update')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (44, 4, N'Permission', N'Attributes.Add')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (45, 4, N'Permission', N'Attributes.Read')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (46, 4, N'Permission', N'Products.Update')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (47, 4, N'Permission', N'Products.Read')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (48, 4, N'Permission', N'Notes.Remove')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (49, 4, N'Permission', N'Notes.Update')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (50, 4, N'Permission', N'Notes.Add')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (51, 4, N'Permission', N'Notes.Read')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (52, 4, N'Permission', N'Counterparties.Remove')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (53, 4, N'Permission', N'Counterparties.Update')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (54, 4, N'Permission', N'Users.Add')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (55, 4, N'Permission', N'Users.Update')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (56, 4, N'Permission', N'Users.Remove')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (57, 4, N'Permission', N'Cities.Read')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (58, 4, N'Permission', N'Roles.Read')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (59, 4, N'Permission', N'Roles.Add')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (60, 4, N'Permission', N'ScanQRCode')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (61, 4, N'Permission', N'Roles.Update')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (62, 4, N'Permission', N'Invoices.Read')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (63, 4, N'Permission', N'Invoices.Add')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (64, 4, N'Permission', N'Invoices.Update')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (65, 4, N'Permission', N'Invoices.Remove')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (66, 4, N'Permission', N'Counterparties.Read')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (67, 4, N'Permission', N'Counterparties.Add')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (68, 4, N'Permission', N'Roles.Remove')
GO
INSERT [dbo].[RoleClaim] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (69, 4, N'Permission', N'SeeDetails')
GO
SET IDENTITY_INSERT [dbo].[RoleClaim] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 
GO
INSERT [dbo].[User] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [Image], [UserStatus], [PasswordHash], [LastLogin], [CreatedAt], [CreatedById], [LastModifiedAt], [LastModifiedById]) VALUES (1, N'pracownik1', NULL, N'pracownik1@test.com', NULL, 0, NULL, N'3421cb3b-6942-4488-ae9f-04036c5e4549', NULL, 0, 0, NULL, 0, 0, N'Jan', N'Kowalski', NULL, 0, 0xCD8C29B8DEED323FE1538CFBDB46FC2A2EA61CFD67807F0629708EA2A6E13A2919DEF3C837C4E7F2C8E0067568E3236827DEFB05C9346E476B6E954440A908A7, NULL, CAST(N'2019-01-10T19:33:09.0000000' AS DateTime2), NULL, CAST(N'2019-01-10T19:33:09.0000000' AS DateTime2), NULL)
GO
INSERT [dbo].[User] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [Image], [UserStatus], [PasswordHash], [LastLogin], [CreatedAt], [CreatedById], [LastModifiedAt], [LastModifiedById]) VALUES (2, N'pracownik2', NULL, N'pracownik2@test.com', NULL, 0, NULL, N'0759bb91-c0f4-4704-9b4c-fb3de7ed2f2e', NULL, 0, 0, NULL, 0, 0, N'Henryk', N'Sokol', NULL, 0, 0xCD8C29B8DEED323FE1538CFBDB46FC2A2EA61CFD67807F0629708EA2A6E13A2919DEF3C837C4E7F2C8E0067568E3236827DEFB05C9346E476B6E954440A908A7, NULL, CAST(N'2019-01-10T19:33:09.0000000' AS DateTime2), NULL, CAST(N'2019-01-10T19:33:09.0000000' AS DateTime2), NULL)
GO
INSERT [dbo].[User] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [Image], [UserStatus], [PasswordHash], [LastLogin], [CreatedAt], [CreatedById], [LastModifiedAt], [LastModifiedById]) VALUES (3, N'ksiegowy1', NULL, N'ksiegowy1@test.com', NULL, 0, NULL, N'd8681133-8c1c-4a22-b617-0507af93b7f0', NULL, 0, 0, NULL, 0, 0, N'Basia', N'Wronkiewicz', NULL, 0, 0xCD8C29B8DEED323FE1538CFBDB46FC2A2EA61CFD67807F0629708EA2A6E13A2919DEF3C837C4E7F2C8E0067568E3236827DEFB05C9346E476B6E954440A908A7, NULL, CAST(N'2019-01-10T19:33:09.0000000' AS DateTime2), NULL, CAST(N'2019-01-10T19:33:09.0000000' AS DateTime2), NULL)
GO
INSERT [dbo].[User] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [Image], [UserStatus], [PasswordHash], [LastLogin], [CreatedAt], [CreatedById], [LastModifiedAt], [LastModifiedById]) VALUES (4, N'kierownik1', NULL, N'kierownik1@test.com', NULL, 0, NULL, N'3be6b8cf-80a0-4041-9066-73a8ed1cd81d', NULL, 0, 0, NULL, 0, 0, N'Ania', N'Chrzaszcz', NULL, 0, 0xCD8C29B8DEED323FE1538CFBDB46FC2A2EA61CFD67807F0629708EA2A6E13A2919DEF3C837C4E7F2C8E0067568E3236827DEFB05C9346E476B6E954440A908A7, NULL, CAST(N'2019-01-10T19:33:09.0000000' AS DateTime2), NULL, CAST(N'2019-01-10T19:33:09.0000000' AS DateTime2), NULL)
GO
INSERT [dbo].[User] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [Image], [UserStatus], [PasswordHash], [LastLogin], [CreatedAt], [CreatedById], [LastModifiedAt], [LastModifiedById]) VALUES (5, N'kierownik2', NULL, N'kierownik2@test.com', NULL, 0, NULL, N'2b98d01b-9c51-4cdf-936f-d889eb7132e1', NULL, 0, 0, NULL, 0, 0, N'Miroslaw', N'Kamczatka', NULL, 0, 0xCD8C29B8DEED323FE1538CFBDB46FC2A2EA61CFD67807F0629708EA2A6E13A2919DEF3C837C4E7F2C8E0067568E3236827DEFB05C9346E476B6E954440A908A7, NULL, CAST(N'2019-01-10T19:33:09.0000000' AS DateTime2), NULL, CAST(N'2019-01-10T19:33:09.0000000' AS DateTime2), NULL)
GO
INSERT [dbo].[User] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [Image], [UserStatus], [PasswordHash], [LastLogin], [CreatedAt], [CreatedById], [LastModifiedAt], [LastModifiedById]) VALUES (6, N'admin1', N'ADMIN1', N'admin1@test.com', N'ADMIN1@TEST.COM', 0, N'e841813e-4ed1-48ef-baa4-435dcdf45feb', N'69ba43f3-6589-4bff-b64b-92ea7c829ded', NULL, 0, 0, NULL, 0, 0, N'Michal', N'Swierzynka', NULL, 0, 0xCD8C29B8DEED323FE1538CFBDB46FC2A2EA61CFD67807F0629708EA2A6E13A2919DEF3C837C4E7F2C8E0067568E3236827DEFB05C9346E476B6E954440A908A7, CAST(N'2019-01-11T19:11:18.4243482' AS DateTime2), CAST(N'2019-01-10T19:33:09.0000000' AS DateTime2), NULL, CAST(N'2019-01-10T19:33:09.0000000' AS DateTime2), NULL)
GO
INSERT [dbo].[User] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [Image], [UserStatus], [PasswordHash], [LastLogin], [CreatedAt], [CreatedById], [LastModifiedAt], [LastModifiedById]) VALUES (7, N'admin2', NULL, N'admin2@test.com', NULL, 0, NULL, N'13c42195-0931-454d-bef5-29126f0b3add', NULL, 0, 0, NULL, 0, 0, N'Czeslaw', N'Samczyk', NULL, 0, 0xCD8C29B8DEED323FE1538CFBDB46FC2A2EA61CFD67807F0629708EA2A6E13A2919DEF3C837C4E7F2C8E0067568E3236827DEFB05C9346E476B6E954440A908A7, NULL, CAST(N'2019-01-10T19:33:09.0000000' AS DateTime2), NULL, CAST(N'2019-01-10T19:33:09.0000000' AS DateTime2), NULL)
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
INSERT [dbo].[UserRole] ([UserId], [RoleId]) VALUES (1, 1)
GO
INSERT [dbo].[UserRole] ([UserId], [RoleId]) VALUES (2, 1)
GO
INSERT [dbo].[UserRole] ([UserId], [RoleId]) VALUES (3, 2)
GO
INSERT [dbo].[UserRole] ([UserId], [RoleId]) VALUES (4, 3)
GO
INSERT [dbo].[UserRole] ([UserId], [RoleId]) VALUES (5, 3)
GO
INSERT [dbo].[UserRole] ([UserId], [RoleId]) VALUES (6, 4)
GO
INSERT [dbo].[UserRole] ([UserId], [RoleId]) VALUES (7, 4)
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [AK_RoleClaim_Id_ClaimType_ClaimValue]    Script Date: 12.01.2019 23:21:11 ******/
ALTER TABLE [dbo].[RoleClaim] ADD  CONSTRAINT [AK_RoleClaim_Id_ClaimType_ClaimValue] UNIQUE NONCLUSTERED 
(
	[Id] ASC,
	[ClaimType] ASC,
	[ClaimValue] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [AK_UserClaim_Id_ClaimType_ClaimValue]    Script Date: 12.01.2019 23:21:11 ******/
ALTER TABLE [dbo].[UserClaim] ADD  CONSTRAINT [AK_UserClaim_Id_ClaimType_ClaimValue] UNIQUE NONCLUSTERED 
(
	[Id] ASC,
	[ClaimType] ASC,
	[ClaimValue] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Attribute] ADD  DEFAULT (getutcdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Attribute] ADD  DEFAULT (getutcdate()) FOR [LastModifiedAt]
GO
ALTER TABLE [dbo].[Attribute] ADD  DEFAULT ((0)) FOR [Order]
GO
ALTER TABLE [dbo].[City] ADD  DEFAULT (getutcdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[City] ADD  DEFAULT (getutcdate()) FOR [LastModifiedAt]
GO
ALTER TABLE [dbo].[Counterparties] ADD  DEFAULT (getutcdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Counterparties] ADD  DEFAULT (getutcdate()) FOR [LastModifiedAt]
GO
ALTER TABLE [dbo].[GoodsDispatchedNote] ADD  DEFAULT (getutcdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[GoodsDispatchedNote] ADD  DEFAULT (getutcdate()) FOR [LastModifiedAt]
GO
ALTER TABLE [dbo].[GoodsReceivedNote] ADD  DEFAULT (getutcdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[GoodsReceivedNote] ADD  DEFAULT (getutcdate()) FOR [LastModifiedAt]
GO
ALTER TABLE [dbo].[Invoice] ADD  DEFAULT (getutcdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Invoice] ADD  DEFAULT (getutcdate()) FOR [LastModifiedAt]
GO
ALTER TABLE [dbo].[Location] ADD  DEFAULT (getutcdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Location] ADD  DEFAULT (getutcdate()) FOR [LastModifiedAt]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT (getutcdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT (getutcdate()) FOR [LastModifiedAt]
GO
ALTER TABLE [dbo].[Role] ADD  DEFAULT (getutcdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Role] ADD  DEFAULT (getutcdate()) FOR [LastModifiedAt]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT (getutcdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT (getutcdate()) FOR [LastModifiedAt]
GO
ALTER TABLE [dbo].[Attribute]  WITH CHECK ADD  CONSTRAINT [FK_Attribute_User_CreatedById] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Attribute] CHECK CONSTRAINT [FK_Attribute_User_CreatedById]
GO
ALTER TABLE [dbo].[Attribute]  WITH CHECK ADD  CONSTRAINT [FK_Attribute_User_LastModifiedById] FOREIGN KEY([LastModifiedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Attribute] CHECK CONSTRAINT [FK_Attribute_User_LastModifiedById]
GO
ALTER TABLE [dbo].[City]  WITH CHECK ADD  CONSTRAINT [FK_City_User_CreatedById] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[City] CHECK CONSTRAINT [FK_City_User_CreatedById]
GO
ALTER TABLE [dbo].[City]  WITH CHECK ADD  CONSTRAINT [FK_City_User_LastModifiedById] FOREIGN KEY([LastModifiedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[City] CHECK CONSTRAINT [FK_City_User_LastModifiedById]
GO
ALTER TABLE [dbo].[Counterparties]  WITH CHECK ADD  CONSTRAINT [FK_Counterparties_City_CityId] FOREIGN KEY([CityId])
REFERENCES [dbo].[City] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Counterparties] CHECK CONSTRAINT [FK_Counterparties_City_CityId]
GO
ALTER TABLE [dbo].[Counterparties]  WITH CHECK ADD  CONSTRAINT [FK_Counterparties_User_CreatedById] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Counterparties] CHECK CONSTRAINT [FK_Counterparties_User_CreatedById]
GO
ALTER TABLE [dbo].[Counterparties]  WITH CHECK ADD  CONSTRAINT [FK_Counterparties_User_LastModifiedById] FOREIGN KEY([LastModifiedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Counterparties] CHECK CONSTRAINT [FK_Counterparties_User_LastModifiedById]
GO
ALTER TABLE [dbo].[Entries]  WITH CHECK ADD  CONSTRAINT [FK_Entries_Invoice_InvoiceId] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoice] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Entries] CHECK CONSTRAINT [FK_Entries_Invoice_InvoiceId]
GO
ALTER TABLE [dbo].[GoodsDispatchedNote]  WITH CHECK ADD  CONSTRAINT [FK_GoodsDispatchedNote_Invoice_InvoiceId] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoice] ([Id])
GO
ALTER TABLE [dbo].[GoodsDispatchedNote] CHECK CONSTRAINT [FK_GoodsDispatchedNote_Invoice_InvoiceId]
GO
ALTER TABLE [dbo].[GoodsDispatchedNote]  WITH CHECK ADD  CONSTRAINT [FK_GoodsDispatchedNote_User_CreatedById] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[GoodsDispatchedNote] CHECK CONSTRAINT [FK_GoodsDispatchedNote_User_CreatedById]
GO
ALTER TABLE [dbo].[GoodsDispatchedNote]  WITH CHECK ADD  CONSTRAINT [FK_GoodsDispatchedNote_User_LastModifiedById] FOREIGN KEY([LastModifiedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[GoodsDispatchedNote] CHECK CONSTRAINT [FK_GoodsDispatchedNote_User_LastModifiedById]
GO
ALTER TABLE [dbo].[GoodsReceivedNote]  WITH CHECK ADD  CONSTRAINT [FK_GoodsReceivedNote_Invoice_InvoiceId] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoice] ([Id])
GO
ALTER TABLE [dbo].[GoodsReceivedNote] CHECK CONSTRAINT [FK_GoodsReceivedNote_Invoice_InvoiceId]
GO
ALTER TABLE [dbo].[GoodsReceivedNote]  WITH CHECK ADD  CONSTRAINT [FK_GoodsReceivedNote_User_CreatedById] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[GoodsReceivedNote] CHECK CONSTRAINT [FK_GoodsReceivedNote_User_CreatedById]
GO
ALTER TABLE [dbo].[GoodsReceivedNote]  WITH CHECK ADD  CONSTRAINT [FK_GoodsReceivedNote_User_LastModifiedById] FOREIGN KEY([LastModifiedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[GoodsReceivedNote] CHECK CONSTRAINT [FK_GoodsReceivedNote_User_LastModifiedById]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_City_CityId] FOREIGN KEY([CityId])
REFERENCES [dbo].[City] ([Id])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_City_CityId]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_Counterparties_CounterpartyId] FOREIGN KEY([CounterpartyId])
REFERENCES [dbo].[Counterparties] ([Id])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_Counterparties_CounterpartyId]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_User_CreatedById] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_User_CreatedById]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_User_LastModifiedById] FOREIGN KEY([LastModifiedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_User_LastModifiedById]
GO
ALTER TABLE [dbo].[Location]  WITH CHECK ADD  CONSTRAINT [FK_Location_User_CreatedById] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Location] CHECK CONSTRAINT [FK_Location_User_CreatedById]
GO
ALTER TABLE [dbo].[Location]  WITH CHECK ADD  CONSTRAINT [FK_Location_User_LastModifiedById] FOREIGN KEY([LastModifiedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Location] CHECK CONSTRAINT [FK_Location_User_LastModifiedById]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_User_CreatedById] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_User_CreatedById]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_User_LastModifiedById] FOREIGN KEY([LastModifiedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_User_LastModifiedById]
GO
ALTER TABLE [dbo].[ProductAttribute]  WITH CHECK ADD  CONSTRAINT [FK_ProductAttribute_Attribute_AttributeId] FOREIGN KEY([AttributeId])
REFERENCES [dbo].[Attribute] ([Id])
GO
ALTER TABLE [dbo].[ProductAttribute] CHECK CONSTRAINT [FK_ProductAttribute_Attribute_AttributeId]
GO
ALTER TABLE [dbo].[ProductAttribute]  WITH CHECK ADD  CONSTRAINT [FK_ProductAttribute_Product_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductAttribute] CHECK CONSTRAINT [FK_ProductAttribute_Product_ProductId]
GO
ALTER TABLE [dbo].[ProductAttribute]  WITH CHECK ADD  CONSTRAINT [FK_ProductAttribute_User_CreatedById] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[ProductAttribute] CHECK CONSTRAINT [FK_ProductAttribute_User_CreatedById]
GO
ALTER TABLE [dbo].[ProductAttribute]  WITH CHECK ADD  CONSTRAINT [FK_ProductAttribute_User_LastModifiedById] FOREIGN KEY([LastModifiedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[ProductAttribute] CHECK CONSTRAINT [FK_ProductAttribute_User_LastModifiedById]
GO
ALTER TABLE [dbo].[ProductDetail]  WITH CHECK ADD  CONSTRAINT [FK_ProductDetail_Location_LocationId] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Location] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductDetail] CHECK CONSTRAINT [FK_ProductDetail_Location_LocationId]
GO
ALTER TABLE [dbo].[ProductDetail]  WITH CHECK ADD  CONSTRAINT [FK_ProductDetail_Product_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductDetail] CHECK CONSTRAINT [FK_ProductDetail_Product_ProductId]
GO
ALTER TABLE [dbo].[ProductDetail]  WITH CHECK ADD  CONSTRAINT [FK_ProductDetail_User_CreatedById] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[ProductDetail] CHECK CONSTRAINT [FK_ProductDetail_User_CreatedById]
GO
ALTER TABLE [dbo].[ProductDetail]  WITH CHECK ADD  CONSTRAINT [FK_ProductDetail_User_LastModifiedById] FOREIGN KEY([LastModifiedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[ProductDetail] CHECK CONSTRAINT [FK_ProductDetail_User_LastModifiedById]
GO
ALTER TABLE [dbo].[Role]  WITH CHECK ADD  CONSTRAINT [FK_Role_User_CreatedById] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Role] CHECK CONSTRAINT [FK_Role_User_CreatedById]
GO
ALTER TABLE [dbo].[Role]  WITH CHECK ADD  CONSTRAINT [FK_Role_User_LastModifiedById] FOREIGN KEY([LastModifiedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Role] CHECK CONSTRAINT [FK_Role_User_LastModifiedById]
GO
ALTER TABLE [dbo].[RoleClaim]  WITH CHECK ADD  CONSTRAINT [FK_RoleClaim_Role_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleClaim] CHECK CONSTRAINT [FK_RoleClaim_Role_RoleId]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_User_CreatedById] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_User_CreatedById]
GO
ALTER TABLE [dbo].[UserClaim]  WITH CHECK ADD  CONSTRAINT [FK_UserClaim_User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserClaim] CHECK CONSTRAINT [FK_UserClaim_User_UserId]
GO
ALTER TABLE [dbo].[UserLogin]  WITH CHECK ADD  CONSTRAINT [FK_UserLogin_User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserLogin] CHECK CONSTRAINT [FK_UserLogin_User_UserId]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_Role_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_Role_RoleId]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_User_UserId]
GO
ALTER TABLE [dbo].[UserToken]  WITH CHECK ADD  CONSTRAINT [FK_UserToken_User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserToken] CHECK CONSTRAINT [FK_UserToken_User_UserId]
GO


SET IDENTITY_INSERT [dbo].[Attribute] ON 
GO
INSERT [dbo].[Attribute] ([Id], [Name], [Order]) VALUES (1, N'Firma', 0)
GO
INSERT [dbo].[Attribute] ([Id], [Name], [Order]) VALUES (2, N'Kategoria', 0)
GO
INSERT [dbo].[Attribute] ([Id], [Name], [Order]) VALUES (3, N'Wymiary', 0)
GO
INSERT [dbo].[Attribute] ([Id], [Name], [Order]) VALUES (4, N'Kolor', 0)
GO
INSERT [dbo].[Attribute] ([Id], [Name], [Order]) VALUES (5, N'Rozmiar', 0)
GO
SET IDENTITY_INSERT [dbo].[Attribute] OFF
GO
SET IDENTITY_INSERT [dbo].[City] ON 
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (146, N'Augustów')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (175, N'Bartoszyce')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (73, N'Bełchatów')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (75, N'Będzin')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (76, N'Biała')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (172, N'Białogard')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (11, N'Białystok')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (141, N'Bielawa')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (167, N'Bielsk')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (22, N'Bielsko')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (164, N'Biłgoraj')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (147, N'Bochnia')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (112, N'Bolesławiec')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (156, N'Brodnica')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (124, N'Brzeg')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (8, N'Bydgoszcz')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (23, N'Bytom')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (59, N'Chełm')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (107, N'Chojnice')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (36, N'Chorzów')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (119, N'Chrzanów')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (101, N'Ciechanów')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (127, N'Cieszyn')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (129, N'Czechowice')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (135, N'Czeladź')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (157, N'Czerwionka')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (13, N'Częstochowa')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (28, N'Dąbrowa')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (96, N'Dębica')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (131, N'Dzierżoniów')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (29, N'Elbląg')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (71, N'Ełk')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (6, N'Gdańsk')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (12, N'Gdynia')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (149, N'Giżycko')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (18, N'Gliwice')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (56, N'Głogów')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (53, N'Gniezno')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (188, N'Goleniów')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (158, N'Gorlice')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (27, N'Gorzów')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (150, N'Grodzisk')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (40, N'Grudziądz')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (134, N'Iława')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (49, N'Inowrocław')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (168, N'Jarocin')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (113, N'Jarosław')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (126, N'Jasło')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (43, N'Jastrzębie')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (180, N'Jawor')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (42, N'Jaworzno')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (44, N'Jelenia')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (38, N'Kalisz')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (10, N'Katowice')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (65, N'Kędzierzyn')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (160, N'Kętrzyn')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (17, N'Kielce')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (174, N'Kluczbork')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (161, N'Kłodzko')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (114, N'Knurów')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (185, N'Koło')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (95, N'Kołobrzeg')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (45, N'Konin')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (37, N'Koszalin')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (179, N'Kościan')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (181, N'Kościerzyna')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (2, N'Kraków')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (128, N'Kraśnik')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (94, N'Krosno')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (153, N'Krotoszyn')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (97, N'Kutno')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (117, N'Kwidzyn')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (80, N'Legionowo')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (39, N'Legnica')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (62, N'Leszno')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (130, N'Lębork')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (191, N'Lubartów')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (51, N'Lubin')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (9, N'Lublin')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (177, N'Lubliniec')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (145, N'Luboń')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (193, N'Łaziska')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (66, N'Łomża')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (152, N'Łowicz')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (3, N'Łódź')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (144, N'Łuków')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (116, N'Malbork')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (154, N'Marki')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (68, N'Mielec')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (110, N'Mikołów')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (109, N'Mińsk')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (143, N'Mława')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (48, N'Mysłowice')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (136, N'Myszków')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (99, N'Nysa')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (123, N'Oleśnica')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (125, N'Olkusz')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (21, N'Olsztyn')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (137, N'Oława')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (31, N'Opole')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (81, N'Ostrołęka')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (52, N'Ostrowiec')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (132, N'Ostróda')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (187, N'Ostrów')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (111, N'Oświęcim')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (98, N'Otwock')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (58, N'Pabianice')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (100, N'Piaseczno')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (186, N'Piastów')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (78, N'Piekary')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (50, N'Piła')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (47, N'Piotrków')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (30, N'Płock')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (192, N'Płońsk')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (133, N'Police')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (189, N'Polkowice')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (5, N'Poznań')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (155, N'Pruszcz')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (72, N'Pruszków')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (64, N'Przemyśl')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (170, N'Pszczyna')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (86, N'Puławy')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (79, N'Racibórz')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (14, N'Radom')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (91, N'Radomsko')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (184, N'Reda')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (24, N'Ruda')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (93, N'Rumia')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (25, N'Rybnik')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (19, N'Rzeszów')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (173, N'Sandomierz')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (115, N'Sanok')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (46, N'Siedlce')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (57, N'Siemianowice')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (103, N'Sieradz')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (92, N'Skarżysko')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (178, N'Skawina')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (88, N'Skierniewice')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (41, N'Słupsk')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (122, N'Sochaczew')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (120, N'Sopot')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (15, N'Sosnowiec')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (63, N'Stalowa')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (84, N'Starachowice')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (54, N'Stargard')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (89, N'Starogard')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (55, N'Suwałki')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (142, N'Swarzędz')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (7, N'Szczecin')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (106, N'Szczecinek')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (176, N'Szczytno')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (148, N'Śrem')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (190, N'Środa')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (74, N'Świdnica')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (108, N'Świdnik')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (183, N'Świebodzice')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (166, N'Świecie')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (82, N'Świętochłowice')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (104, N'Świnoujście')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (90, N'Tarnobrzeg')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (69, N'Tarnowskie')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (35, N'Tarnów')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (70, N'Tczew')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (61, N'Tomaszów')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (16, N'Toruń')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (159, N'Turek')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (26, N'Tychy')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (33, N'Wałbrzych')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (169, N'Wałcz')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (1, N'Warszawa')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (171, N'Wągrowiec')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (85, N'Wejherowo')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (182, N'Wieluń')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (34, N'Włocławek')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (87, N'Wodzisław')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (121, N'Wołomin')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (4, N'Wrocław')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (151, N'Września')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (163, N'Wyszków')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (20, N'Zabrze')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (162, N'Zakopane')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (194, N'Zambrów')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (60, N'Zamość')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (83, N'Zawiercie')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (140, N'Ząbki')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (102, N'Zduńska')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (77, N'Zgierz')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (139, N'Zgorzelec')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (32, N'Zielona')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (165, N'Żagań')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (118, N'Żary')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (67, N'Żory')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (105, N'Żyrardów')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (138, N'Żywiec')
GO
SET IDENTITY_INSERT [dbo].[City] OFF
GO
SET IDENTITY_INSERT [dbo].[Counterparties] ON 
GO
INSERT [dbo].[Counterparties] ([Id], [Name], [PostalCode], [Street], [CityId], [PhoneNumber], [NIP]) VALUES (1, N'Sklep Odziezowy Spodniex', N'02-376', N'Generała J. 10', 3, NULL, N'9324472736')
GO
INSERT [dbo].[Counterparties] ([Id], [Name], [PostalCode], [Street], [CityId], [PhoneNumber], [NIP]) VALUES (2, N'Sklep Odziezowy Garnitury', N'01-212', N'Mickiewicza 64', 3, NULL, N'1176550095')
GO
INSERT [dbo].[Counterparties] ([Id], [Name], [PostalCode], [Street], [CityId], [PhoneNumber], [NIP]) VALUES (3, N'Sklep Odziezowy Koszuland', N'01-123', N'Partyzantow 12', 1, NULL, N'1164191087')
GO
INSERT [dbo].[Counterparties] ([Id], [Name], [PostalCode], [Street], [CityId], [PhoneNumber], [NIP]) VALUES (4, N'Sklep Obuwiczy Szewcum', N'02-341', N'Maja 52', 2, NULL, N'3390598589')
GO
INSERT [dbo].[Counterparties] ([Id], [Name], [PostalCode], [Street], [CityId], [PhoneNumber], [NIP]) VALUES (5, N'Sklep Obuwiczy Kapciex', N'01-321', N'Kolobrzeska 12', 4, NULL, N'5343283253')
GO
INSERT [dbo].[Counterparties] ([Id], [Name], [PostalCode], [Street], [CityId], [PhoneNumber], [NIP]) VALUES (6, N'Sklep Obuwiczy Podeszwa', N'03-123', N'Pulaskiego 50', 5, NULL, N'5360519132')
GO
INSERT [dbo].[Counterparties] ([Id], [Name], [PostalCode], [Street], [CityId], [PhoneNumber], [NIP]) VALUES (7, N'Lingeries sp. z o. o.', N'03-373', N'Warminska 18', 6, NULL, N'3932688870')
GO
INSERT [dbo].[Counterparties] ([Id], [Name], [PostalCode], [Street], [CityId], [PhoneNumber], [NIP]) VALUES (8, N'Abibas ltd.', N'02-332', N'Rosevelta 4', 7, NULL, N'3427336823')
GO
INSERT [dbo].[Counterparties] ([Id], [Name], [PostalCode], [Street], [CityId], [PhoneNumber], [NIP]) VALUES (9, N'Nikeks ltd', N'02-221', N'Dworcowa 5', 8, NULL, N'5117198606')
GO
INSERT [dbo].[Counterparties] ([Id], [Name], [PostalCode], [Street], [CityId], [PhoneNumber], [NIP]) VALUES (10, N'Pumbina sp. z o. o.', N'03-676', N'Wojska Polskiego 104', 9, NULL, N'3755158504')
GO
INSERT [dbo].[Counterparties] ([Id], [Name], [PostalCode], [Street], [CityId], [PhoneNumber], [NIP]) VALUES (11, N'MyClothing ltd', N'01-245', N'Miła 9', 10, NULL, N'7996890195')
GO
INSERT [dbo].[Counterparties] ([Id], [Name], [PostalCode], [Street], [CityId], [PhoneNumber], [NIP]) VALUES (12, N'CraftyShoes ltd', N'02-376', N'Odyńca 231', 11, NULL, N'3560227864')
GO
SET IDENTITY_INSERT [dbo].[Counterparties] OFF
GO
SET IDENTITY_INSERT [dbo].[Location] ON 
GO
INSERT [dbo].[Location] ([Id], [Name]) VALUES (1, N'Półka 1')
GO
INSERT [dbo].[Location] ([Id], [Name]) VALUES (2, N'Półka 2')
GO
INSERT [dbo].[Location] ([Id], [Name]) VALUES (3, N'Półka 3')
GO
SET IDENTITY_INSERT [dbo].[Location] OFF
GO