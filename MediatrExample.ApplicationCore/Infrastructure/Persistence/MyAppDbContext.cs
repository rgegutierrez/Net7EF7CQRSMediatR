using MediatrExample.ApplicationCore.Domain;
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
}