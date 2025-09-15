CREATE DATABASE [dbAppCurrency];

USE [dbAppCurrency];

GO

if not exists(select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'Currencies')
begin
	
	CREATE TABLE [Currencies] (
			  [Id] int NOT NULL IDENTITY,
			  [Base] nvarchar(max) NOT NULL,
			  [Description] nvarchar(max) NOT NULL,
			  [Type] nvarchar(max) NOT NULL,
			  [CreatedAt] datetime2 NOT NULL,
			  CONSTRAINT [PK_Currencies] PRIMARY KEY ([Id])
		  );
end;

if not exists(select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'LogBooks')
begin
	
	CREATE TABLE [LogBooks] (
			  [Id] uniqueidentifier NOT NULL,
			  [CreatedAt] datetime2 NOT NULL,
			  [Message] nvarchar(max) NOT NULL,
			  [StackTrace] nvarchar(max) NOT NULL,
			  [Source] nvarchar(max) NOT NULL,
			  CONSTRAINT [PK_LogBooks] PRIMARY KEY ([Id])
		  );
end;

 

if not exists(select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'Requests')
begin

	CREATE TABLE [Requests] (
			  [Id] uniqueidentifier NOT NULL,
			  [ControllerName] nvarchar(max) NOT NULL,
			  [ActionName] nvarchar(max) NOT NULL,
			  [Method] nvarchar(max) NOT NULL,
			  [Protocol] nvarchar(max) NOT NULL,
			  [Host] nvarchar(max) NOT NULL,
			  [Path] nvarchar(max) NOT NULL,
			  [CreatedAt] datetime2 NOT NULL,
			  [ContentType] nvarchar(max) NOT NULL,
			  [Data] nvarchar(max) NOT NULL,
			  CONSTRAINT [PK_Requests] PRIMARY KEY ([Id])
		  );
end;