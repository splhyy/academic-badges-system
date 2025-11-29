using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BadgeRepository : BaseRepository<Badge>, IBadgeRepository
{
    public BadgeRepository(InMemoryDbContext context) : base(context) { }

    public async Task<Badge?> ObterPorNomeAsync(string nome)
    {
        return await _dbSet.FirstOrDefaultAsync(b => b.Nome == nome);
    }

    public async Task<IEnumerable<Badge>> ObterBadgesPorDificuldadeAsync(int dificuldadeMinima, int dificuldadeMaxima)
    {
        return await _dbSet
            .Where(b => b.Dificuldade >= dificuldadeMinima && b.Dificuldade <= dificuldadeMaxima)
            .ToListAsync();
    }
}