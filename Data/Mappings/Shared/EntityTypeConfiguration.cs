using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings;

public abstract class EntityTypeConfiguration<T> where T : Entity
{
    public static void ConfigureBase(EntityTypeBuilder<T> builder)
    {
        builder.Property(x => x.UsuarioCriacao)
            .HasColumnType("varchar(250)");

        builder.Property(x => x.DataCriacao)
            .HasColumnType("datetime");

        builder.Property(x => x.UsuarioAlteracao)
            .HasColumnType("varchar(250)");

        builder.Property(x => x.DataAlteracao)
            .HasColumnType("datetime");
    }
}
