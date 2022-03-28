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

CREATE TABLE [cliente] (
    [Id] int NOT NULL IDENTITY,
    [Nombre] nvarchar(max) NOT NULL,
    [Apellido] nvarchar(max) NOT NULL,
    [NumeroIdentificacion] int NOT NULL,
    [Direccion] nvarchar(max) NOT NULL,
    [Telefono] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_cliente] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [producto] (
    [Id] int NOT NULL IDENTITY,
    [Nombre] nvarchar(max) NOT NULL,
    [ValorVentaConIVA] decimal(18,2) NOT NULL,
    [CantidadUnidadesIventario] int NOT NULL,
    [PorcentajeIVAAplicado] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_producto] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [detalleFactura] (
    [id] int NOT NULL IDENTITY,
    [CantidadUnidades] int NOT NULL,
    [ValorUnitarioSinIVA] decimal(18,2) NOT NULL,
    [valorUnitarioconIVA] decimal(18,2) NOT NULL,
    [ValorTotalCompra] decimal(18,2) NOT NULL,
    [ProductoId] int NOT NULL,
    CONSTRAINT [PK_detalleFactura] PRIMARY KEY ([id]),
    CONSTRAINT [FK_detalleFactura_producto_ProductoId] FOREIGN KEY ([ProductoId]) REFERENCES [producto] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [factura] (
    [Id] int NOT NULL IDENTITY,
    [FechaVenta] datetime2 NOT NULL,
    [TotalPrecioVenta] decimal(18,2) NOT NULL,
    [SubTotalSinIVA] decimal(18,2) NOT NULL,
    [FechaEntrega] datetime2 NOT NULL,
    [ClienteId] int NOT NULL,
    [DetalleFacturaid] int NULL,
    CONSTRAINT [PK_factura] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_factura_cliente_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [cliente] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_factura_detalleFactura_DetalleFacturaid] FOREIGN KEY ([DetalleFacturaid]) REFERENCES [detalleFactura] ([id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_detalleFactura_ProductoId] ON [detalleFactura] ([ProductoId]);
GO

CREATE INDEX [IX_factura_ClienteId] ON [factura] ([ClienteId]);
GO

CREATE INDEX [IX_factura_DetalleFacturaid] ON [factura] ([DetalleFacturaid]);
GO

INSERT INTO [dbo].[Producto] VALUES ('Audifonos',320000,1000,0.16)
                INSERT INTO [dbo].[Producto] VALUES ('Mouse',118000,1000,0.16)
                INSERT INTO [dbo].[Producto] VALUES ('Teclado',80000,1000,0.16)
                INSERT INTO [dbo].[Producto] VALUES ('Monitor',875000,1000,0.16)
                INSERT INTO [dbo].[Producto] VALUES ('Windows 10 Enterprise Licence',1250000,1000,0.16)
                INSERT INTO [dbo].[Producto] VALUES ('ThinkPAd 395 ',475000,1000,0.16)
                INSERT INTO [dbo].[Producto] VALUES ('ASUS L790',2850000,1000,0.16)
                INSERT INTO [dbo].[Producto] VALUES ('ACER T890',320000,1000,0.16)
                INSERT INTO [dbo].[Producto] VALUES ('MSI 750',6500000,1000,0.16)
                INSERT INTO [dbo].[Producto] VALUES ('Lenovo A100',3500000,1000,0.16)
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220328141047_InitialCreate', N'6.0.0-preview.5.21301.9');
GO

COMMIT;
GO

