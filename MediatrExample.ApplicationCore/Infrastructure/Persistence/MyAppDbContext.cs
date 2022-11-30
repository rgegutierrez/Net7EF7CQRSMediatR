using MediatrExample.ApplicationCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MediatrExample.ApplicationCore.Infrastructure.Persistence;
public class MyAppDbContext : DbContext
{
    public MyAppDbContext(DbContextOptions<MyAppDbContext> options) : base(options)
    {

    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<MateriaPrima> MateriasPrimas => Set<MateriaPrima>();
}