IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Clients] (
    [Id] int NOT NULL IDENTITY,
    [Name] VARCHAR(80) NOT NULL,
    [Phone] CHAR(11) NULL,
    [CEP] CHAR(8) NOT NULL,
    [State] CHAR(2) NOT NULL,
    [City] nvarchar(60) NOT NULL,
    CONSTRAINT [PK_Clients] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Products] (
    [Id] int NOT NULL IDENTITY,
    [BarCode] VARCHAR(14) NOT NULL,
    [Description] VARCHAR(60) NULL,
    [Value] decimal(18,2) NOT NULL,
    [ProductType] nvarchar(max) NOT NULL,
    [Active] bit NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Orders] (
    [Id] int NOT NULL IDENTITY,
    [ClientId] int NOT NULL,
    [CreatedAt] datetime2 NOT NULL DEFAULT (GETDATE()),
    [FinishedAt] datetime2 NOT NULL,
    [FreightType] int NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    [Observation] VARCHAR(512) NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Orders_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Clients] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [OrderItems] (
    [Id] int NOT NULL IDENTITY,
    [OrderId] int NOT NULL,
    [ProductId] int NOT NULL,
    [Quantity] int NOT NULL DEFAULT 1,
    [Value] decimal(18,2) NOT NULL,
    [Discount] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_OrderItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OrderItems_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_OrderItems_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [idx_client_phone] ON [Clients] ([Phone]);
GO

CREATE INDEX [IX_OrderItems_OrderId] ON [OrderItems] ([OrderId]);
GO

CREATE INDEX [IX_OrderItems_ProductId] ON [OrderItems] ([ProductId]);
GO

CREATE INDEX [IX_Orders_ClientId] ON [Orders] ([ClientId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210723020102_PrimeiraMigracao', N'5.0.8');
GO

COMMIT;
GO

