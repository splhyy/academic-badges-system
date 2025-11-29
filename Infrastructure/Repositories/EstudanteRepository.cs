using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EstudanteRepository : BaseRepository<Estudante>, IEstudanteRepository
{
    public EstudanteRepository(InMemoryDbContext context) : base(context) { }

    public async Task<Estudante?> ObterPorEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(e => e.Email == email);
    }

    public async Task<bool> EmailExisteAsync(string email)
    {
        return await _dbSet.AnyAsync(e => e.Email == email);
    }

    public async Task<IEnumerable<Badge>> ObterBadgesDoEstudanteAsync(Guid estudanteId)
    {
        var estudante = await _dbSet
            .Include(e => e.Badges)
            .FirstOrDefaultAsync(e => e.Id == estudanteId);
            
        return estudante?.Badges ?? Enumerable.Empty<Badge>();
    }

    public async Task<IEnumerable<MissaoConcluida>> ObterMissoesConcluidasAsync(Guid estudanteId)
    {
        var estudante = await _dbSet
            .Include(e => e.MissoesConcluidas)
            .FirstOrDefaultAsync(e => e.Id == estudanteId);
            
        return estudante?.MissoesConcluidas ?? Enumerable.Empty<MissaoConcluida>();
    }

    public async Task<Estudante?> ObterComBadgesEMissoesAsync(Guid estudanteId)
    {
        return await _dbSet
            .Include(e => e.Badges)
            .Include(e => e.MissoesConcluidas)
            .FirstOrDefaultAsync(e => e.Id == estudanteId);
    }
}