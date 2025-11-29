using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MissaoRepository : BaseRepository<Missao>, IMissaoRepository
{
    public MissaoRepository(InMemoryDbContext context) : base(context) { }

    public async Task<Missao?> ObterPorTituloAsync(string titulo)
    {
        return await _dbSet.FirstOrDefaultAsync(m => m.Titulo == titulo);
    }

    public async Task<IEnumerable<Missao>> ObterMissoesPorDificuldadeAsync(int dificuldade)
    {
        return await _dbSet
            .Where(m => m.Dificuldade == dificuldade)
            .ToListAsync();
    }

    public async Task<IEnumerable<Missao>> ObterMissoesDisponiveisAsync()
    {
        return await _dbSet
            .Where(m => !m.Concluida)
            .ToListAsync();
    }

    public async Task<IEnumerable<Missao>> ObterMissoesComRecompensaAsync()
    {
        return await _dbSet
            .Where(m => m.BadgeRecompensaId != null)
            .ToListAsync();
    }
}