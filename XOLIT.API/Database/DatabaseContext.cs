using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XOLIT.API.Models;

namespace XOLIT.API.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Factura> factura { get; set; }
        public DbSet<Cliente> cliente { get; set; }
        public DbSet<Producto> producto { get; set; }
        public DbSet<DetalleFactura> detalleFactura  { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FacturaConfiguracion());
            modelBuilder.ApplyConfiguration(new ClienteConfiguracion());
            modelBuilder.ApplyConfiguration(new ProductoConfiguracion());

        }
    }
}
