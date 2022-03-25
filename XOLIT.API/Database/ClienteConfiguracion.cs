using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XOLIT.API.Models;

namespace XOLIT.API.Database
{
    public class ClienteConfiguracion : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Nombre)
                .IsRequired();
            builder.Property(s => s.Apellido)
                .IsRequired();
            builder.Property(s => s.NumeroIdentificacion)
                .IsRequired();
            builder.Property(s => s.Direccion)
                .IsRequired();
            builder.Property(s => s.Telefono)
                .IsRequired();
        }
    }
}
