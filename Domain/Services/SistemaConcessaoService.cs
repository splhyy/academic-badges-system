using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Domain.Services;

public class SistemaConcessaoService
{
    private readonly IEstudanteRepository _estudanteRepository;
    private readonly IBadgeRepository _badgeRepository;
    private readonly IMissaoRepository _missaoRepository;

    public SistemaConcessaoService(
        IEstudanteRepository estudanteRepository,
        IBadgeRepository badgeRepository,
        IMissaoRepository missaoRepository)
    {
        _estudanteRepository = estudanteRepository;
        _badgeRepository = badgeRepository;
        _missaoRepository = missaoRepository;
    }

    public async Task ConcederBadgePorMissaoAsync(Guid estudanteId, Guid missaoId)
    {
        var estudante = await _estudanteRepository.ObterComBadgesEMissoesAsync(estudanteId);
        var missao = await _missaoRepository.ObterPorIdAsync(missaoId);

        if (estudante == null)
            throw new SistemaConcessaoException("Estudante não encontrado");
            
        if (missao == null)
            throw new SistemaConcessaoException("Missão não encontrada");

        if (!estudante.ConcluiuMissao(missaoId))
            throw new SistemaConcessaoException("Estudante não concluiu a missão");

        if (missao.BadgeRecompensaId == null)
            throw new SistemaConcessaoException("Missão não possui badge de recompensa");

        var badge = await _badgeRepository.ObterPorIdAsync(missao.BadgeRecompensaId.Value);
        
        if (badge == null)
            throw new SistemaConcessaoException("Badge de recompensa não encontrada");

        if (estudante.PossuiBadge(badge.Id))
            throw new SistemaConcessaoException("Estudante já possui esta badge");

        estudante.AdicionarBadge(badge);
        await _estudanteRepository.AtualizarAsync(estudante);
    }

    public async Task<int> ObterPontuacaoTotalAsync(Guid estudanteId)
    {
        var estudante = await _estudanteRepository.ObterPorIdAsync(estudanteId);
        return estudante?.PontuacaoTotal ?? 0;
    }

    public async Task<IEnumerable<Badge>> ObterBadgesDoEstudanteAsync(Guid estudanteId)
    {
        return await _estudanteRepository.ObterBadgesDoEstudanteAsync(estudanteId);
    }

    public async Task ConcluirMissaoEConcederBadgeAsync(Guid estudanteId, Guid missaoId)
    {
        var estudante = await _estudanteRepository.ObterPorIdAsync(estudanteId);
        var missao = await _missaoRepository.ObterPorIdAsync(missaoId);

        if (estudante == null || missao == null)
            throw new SistemaConcessaoException("Estudante ou missão não encontrados");

        estudante.ConcluirMissao(missao);
        await _estudanteRepository.AtualizarAsync(estudante);

        if (missao.BadgeRecompensaId.HasValue)
        {
            await ConcederBadgePorMissaoAsync(estudanteId, missaoId);
        }
    }
}