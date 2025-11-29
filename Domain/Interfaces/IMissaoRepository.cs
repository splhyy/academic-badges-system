using Domain.Entities;

namespace Domain.Interfaces;

public interface IMissaoRepository : IRepository<Missao>
{
    Task<Missao?> ObterPorTituloAsync(string titulo);
    Task<IEnumerable<Missao>> ObterMissoesPorDificuldadeAsync(int dificuldade);
    Task<IEnumerable<Missao>> ObterMissoesDisponiveisAsync();
    Task<IEnumerable<Missao>> ObterMissoesComRecompensaAsync();
}
