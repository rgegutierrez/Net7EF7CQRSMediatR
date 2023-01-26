using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Domain.Receta;
using MediatrExample.ApplicationCore.Domain.View;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

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
    public DbSet<LineaProduccion> LineasProduccion => Set<LineaProduccion>();
    public DbSet<ProductoQuimico> ProductosQuimicos => Set<ProductoQuimico>();
    public DbSet<TiroMaquina> TirosMaquina => Set<TiroMaquina>();
    public DbSet<Formacion> Formaciones => Set<Formacion>();
    public DbSet<TipoReceta> TiposReceta => Set<TipoReceta>();
    public DbSet<RecetaFabricacion> Recetas => Set<RecetaFabricacion>();
    public DbSet<RecetaFabricacionVW> RecetasVW => Set<RecetaFabricacionVW>();
    public DbSet<RecetaLineaProduccion> RecetasLineaProduccion => Set<RecetaLineaProduccion>();
    public DbSet<RecetaMateriaPrima> RecetasMateriaPrima => Set<RecetaMateriaPrima>();
    public DbSet<RecetaLineaPreparacion> RecetasLineaPreparacion => Set<RecetaLineaPreparacion>();
    public DbSet<RecetaPreparacionPasta> RecetasPreparacionPasta => Set<RecetaPreparacionPasta>();
    public DbSet<RecetaLineaMaquina> RecetasLineaMaquina => Set<RecetaLineaMaquina>();
    public DbSet<RecetaMaquinaPapelera> RecetasMaquinaPapelera => Set<RecetaMaquinaPapelera>();
    public DbSet<RecetaVariableFormula> RecetasVariableFormula => Set<RecetaVariableFormula>();
    public DbSet<RecetaProductoQuimico> RecetasProductoQuimico => Set<RecetaProductoQuimico>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder
           .Entity<RecetaFabricacionVW>()
           .ToView("RecetaFabricacionVW")
           .HasKey(t => t.RecetaFabricacionId);

        modelBuilder
            .Entity<RecetaFabricacion>()
            .Property(b => b.Notificacion)
            .HasDefaultValue(0);
    }
}