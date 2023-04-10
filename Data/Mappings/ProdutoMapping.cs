using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings;

public class ProdutoMapping : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Descricao)
            .IsRequired()
            .HasColumnType("varchar(50)");

        EntityTypeConfiguration<Produto>.ConfigureBase(builder);

        builder.ToTable("Produto");
    }
}
