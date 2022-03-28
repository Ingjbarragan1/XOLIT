using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace XOLIT.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cliente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroIdentificacion = table.Column<int>(type: "int", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cliente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "producto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorVentaConIVA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CantidadUnidadesIventario = table.Column<int>(type: "int", nullable: false),
                    PorcentajeIVAAplicado = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_producto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "detalleFactura",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CantidadUnidades = table.Column<int>(type: "int", nullable: false),
                    ValorUnitarioSinIVA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    valorUnitarioconIVA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorTotalCompra = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detalleFactura", x => x.id);
                    table.ForeignKey(
                        name: "FK_detalleFactura_producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "producto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "factura",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaVenta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPrecioVenta = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SubTotalSinIVA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaEntrega = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    DetalleFacturaid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_factura", x => x.Id);
                    table.ForeignKey(
                        name: "FK_factura_cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_factura_detalleFactura_DetalleFacturaid",
                        column: x => x.DetalleFacturaid,
                        principalTable: "detalleFactura",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_detalleFactura_ProductoId",
                table: "detalleFactura",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_factura_ClienteId",
                table: "factura",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_factura_DetalleFacturaid",
                table: "factura",
                column: "DetalleFacturaid");

            migrationBuilder.Sql(@"INSERT INTO [dbo].[Producto] VALUES ('Audifonos',320000,1000,0.16)
                INSERT INTO [dbo].[Producto] VALUES ('Mouse',118000,1000,0.16)
                INSERT INTO [dbo].[Producto] VALUES ('Teclado',80000,1000,0.16)
                INSERT INTO [dbo].[Producto] VALUES ('Monitor',875000,1000,0.16)
                INSERT INTO [dbo].[Producto] VALUES ('Windows 10 Enterprise Licence',1250000,1000,0.16)
                INSERT INTO [dbo].[Producto] VALUES ('ThinkPAd 395 ',475000,1000,0.16)
                INSERT INTO [dbo].[Producto] VALUES ('ASUS L790',2850000,1000,0.16)
                INSERT INTO [dbo].[Producto] VALUES ('ACER T890',320000,1000,0.16)
                INSERT INTO [dbo].[Producto] VALUES ('MSI 750',6500000,1000,0.16)
                INSERT INTO [dbo].[Producto] VALUES ('Lenovo A100',3500000,1000,0.16)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "factura");

            migrationBuilder.DropTable(
                name: "cliente");

            migrationBuilder.DropTable(
                name: "detalleFactura");

            migrationBuilder.DropTable(
                name: "producto");
        }
    }
}
