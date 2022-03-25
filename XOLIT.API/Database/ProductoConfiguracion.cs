using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XOLIT.API.Models;

namespace XOLIT.API.Database
{
    public class ProductoConfiguracion : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Nombre)
                .IsRequired();
            builder.Property(s => s.ValorVentaConIVA)
                .IsRequired();
            builder.Property(s => s.CantidadUnidadesIventario)
                .IsRequired();
            builder.Property(s => s.PorcentajeIVAAplicado)
                .IsRequired();
        }
    }
}
