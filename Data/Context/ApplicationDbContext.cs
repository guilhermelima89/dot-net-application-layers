using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, Role, Guid>
{
    private readonly IUserService _user;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IUserService user) : base(options)
    {
        _user = user;
    }

    public DbSet<ApplicationUser> ApplicationUser { get; set; }
    public DbSet<Produto> Produto { get; set; }
    public DbSet<RefreshToken> RefreshToken { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
        }

        base.OnModelCreating(builder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCriacao") is not null))
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property("UsuarioCriacao").CurrentValue = _user.GetFullName();
                entry.Property("DataCriacao").CurrentValue = DateTime.UtcNow;
                entry.Property("DataAlteracao").IsModified = false;
                entry.Property("UsuarioAlteracao").IsModified = false;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property("UsuarioAlteracao").CurrentValue = _user.GetFullName();
                entry.Property("DataAlteracao").CurrentValue = DateTime.UtcNow;
                entry.Property("DataCriacao").IsModified = false;
                entry.Property("UsuarioCriacao").IsModified = false;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
