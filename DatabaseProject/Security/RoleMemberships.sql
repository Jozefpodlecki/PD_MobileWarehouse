



GO



GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [WarehouseWorker];


GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [NT SERVICE\SQLSERVERAGENT];


GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [NT Service\MSSQLSERVER];


GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [NT SERVICE\Winmgmt];


GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [NT SERVICE\SQLWriter];


GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [DESKTOP-52AVJMH\Józef Podlecki];


GO
ALTER ROLE [db_owner] ADD MEMBER [MobileWarehouseSystemUser];


GO
ALTER ROLE [db_ddladmin] ADD MEMBER [MobileWarehouseSystemUser];


GO
ALTER ROLE [db_datawriter] ADD MEMBER [MobileWarehouseSystemUser];


GO
ALTER ROLE [db_datareader] ADD MEMBER [MobileWarehouseSystemUser];

