using Domain.Entities;

namespace Domain.Interfaces;

public interface IEstudanteRepository : IRepository<Estudante>
{
    Task<Estudante?> ObterPorEmailAsync(string email);
    Task<IEnumerable<Badge>> ObterBadgesDoEstudanteAsync(Guid estudanteId);
    Task<IEnumerable<MissaoConcluida>> ObterMissoesConcluidasAsync(Guid estudanteId);
    Task<bool> EmailExisteAsync(string email);
    Task<Estudante?> ObterComBadgesEMissoesAsync(Guid estudanteId);
}