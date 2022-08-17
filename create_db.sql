USE master
GO
IF NOT EXISTS (
   SELECT name
   FROM sys.databases
   WHERE name = N'BoardgamesEShopDB'
)
CREATE DATABASE [BoardgamesEShopDB]
GO
