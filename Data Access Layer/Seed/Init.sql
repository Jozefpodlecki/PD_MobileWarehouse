SET NOCOUNT ON;

INSERT INTO [dbo].[Role]
(
	[Name],
	[NormalizedName]
)
VALUES
('ADMIN','ADMIN'),
('MANAGER','MANAGER'),
('EMPLOYEE','EMPLOYEE')

INSERT INTO [dbo].[User]
(
	AccessFailedCount,
	EmailConfirmed,
	LockoutEnabled,
	PhoneNumberConfirmed,
	TwoFactorEnabled,
	UserName,
	NormalizedUserName,
	Email,
	NormalizedEmail,
	FirstName,
	LastName,
	Image,
	PasswordHash,
	LastLogin
)
VALUES
(0,1,0,0,0,'admin','admin','admin','admin','admin','admin','',NULL,GETDATE()),
(0,1,0,0,0,'manager','manager','manager','manager','manager','manager','',NULL,GETDATE()),
(0,1,0,0,0,'employee','employee','employee','employee','employee','employee','',NULL,GETDATE())

INSERT INTO [dbo].[UserRole]
(
	UserId,
	RoleId
)
VALUES
(1,1),
(2,2),
(1,3)

INSERT INTO [dbo].[Event]
(
	IsEnabled,
	Name	
)
VALUES
(0,'LOG_IN'),
(0,'LOG_OUT'),
(0,'NEW_PRODUCT'),
(0,'UPDATE_PRODUCT'),
(0,'DELETE_PRODUCT'),
(0,'ADD_ATTRIBUTE'),
(0,'UPDATE ATTRIBUTE'),
(0,'REMOVE_ATTRIBUTE'),
(0,'ADD_USER'),
(0,'UPDATE_USER'),
(0,'REMOVE_USER'),
(0,'ADD_ROLE'),
(0,'UPDATE_ROLE'),
(0,'REMOVE_ROLE')

DECLARE @ProductId INT,
		@AttributeId INT

INSERT INTO [dbo].[Product]
(
	Name
)
VALUES
('Indesit IWSC 51052 C ECO PL')

SET @ProductId = SCOPE_IDENTITY();

INSERT INTO [dbo].[Attribute]
(
	Name
)
VALUES
('Rodzaj Produktu')

SET @AttributeId = SCOPE_IDENTITY

INSERT INTO [dbo].[ProductAttribute]
(
)
VALUES
(@ProductId,@AttributeId)
,'Pralka'),

,'42 x 59,5 x 85 cm','1000 obr/min','5 kg')