
ALTER ROLE [db_datawriter] ADD MEMBER [WarehouseWorker];


GO
ALTER ROLE [db_datareader] ADD MEMBER [WarehouseWorker];


GO
ALTER ROLE [db_accessadmin] ADD MEMBER [WarehouseWorker];

