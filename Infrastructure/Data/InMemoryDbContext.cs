using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Data;

public class InMemoryDbContext : DbContext
{
    public DbSet<Estudante> Estudantes { get; set; }
    public DbSet<Badge> Badges { get; set; }
    public DbSet<Missao> Missoes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("AcademicBadgesDB");
    }
}