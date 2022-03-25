using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XOLIT.API.Models;

namespace XOLIT.API.Database
{
    public class FacturaConfiguracion : IEntityTypeConfiguration<Factura>
    {
        public void Configure(EntityTypeBuilder<Factura> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.FechaVenta)
                .IsRequired();
            builder.Property(s => s.TotalPrecioVenta)
                .IsRequired();
            builder.Property(s => s.SubTotalSinIVA)
                .IsRequired();
            builder.Property(s => s.FechaEntrega)
                .IsRequired();
        }
    }
}
