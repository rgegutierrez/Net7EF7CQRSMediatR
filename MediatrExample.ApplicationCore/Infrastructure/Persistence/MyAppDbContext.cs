using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Domain.View;
using Microsoft.EntityFrameworkCore;

namespace MediatrExample.ApplicationCore.Infrastructure.Persistence;
public class MyAppDbContext : DbContext
{
    public MyAppDbContext(DbContextOptions<MyAppDbContext> options) : base(options)
    {

    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<MateriaPrima> MateriasPrimas => Set<MateriaPrima>();
    public DbSet<PreparacionPasta> PreparacionPastas => Set<PreparacionPasta>();
    public DbSet<MaquinaPapelera> MaquinasPapeleras => Set<MaquinaPapelera>();
    public DbSet<VariableFormula> VariablesFormula => Set<VariableFormula>();
    public DbSet<RecetaFabricacion> Recetas => Set<RecetaFabricacion>();
    public DbSet<RecetaFabricacionVW> RecetasVW => Set<RecetaFabricacionVW>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder
           .Entity<RecetaFabricacionVW>()
           .ToView("RecetaFabricacionVW")
           .HasKey(t => t.RecetaFabricacionId);
    }
}