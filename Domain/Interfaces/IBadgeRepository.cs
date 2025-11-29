using Domain.Entities;

namespace Domain.Interfaces;

public interface IBadgeRepository : IRepository<Badge>
{
    Task<Badge?> ObterPorNomeAsync(string nome);
    Task<IEnumerable<Badge>> ObterBadgesPorDificuldadeAsync(int dificuldadeMinima, int dificuldadeMaxima);
}
