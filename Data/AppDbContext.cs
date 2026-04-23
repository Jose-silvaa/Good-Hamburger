using GoodHamburger.Features.Pedido.Models;
using GoodHamburger.Features.Produtos.Dtos;
using GoodHamburger.Features.Produtos.Models;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburger.Data;

public class AppDbContext : DbContext
{
    public DbSet<Produto> Produtos => Set<Produto>();
    public DbSet<Pedido> Pedidos => Set<Pedido>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
         
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pedido>(e =>
        {
            e.HasKey(p => p.Id);

            e.HasOne(p => p.Sanduiche)
                .WithMany()
                .HasForeignKey(p => p.SanduicheId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasMany(p => p.Acompanhamentos)
                .WithMany();

            e.Navigation(p => p.Acompanhamentos)
                .HasField("_acompanhamentos")
                .UsePropertyAccessMode(PropertyAccessMode.Field);

        });
    }
}