using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Services;
using Moq;

namespace Domain.Tests.Services;

public class SistemaConcessaoServiceTests
{
    private readonly Mock<IEstudanteRepository> _estudanteRepoMock;
    private readonly Mock<IBadgeRepository> _badgeRepoMock;
    private readonly Mock<IMissaoRepository> _missaoRepoMock;
    private readonly SistemaConcessaoService _service;

    public SistemaConcessaoServiceTests()
    {
        _estudanteRepoMock = new Mock<IEstudanteRepository>();
        _badgeRepoMock = new Mock<IBadgeRepository>();
        _missaoRepoMock = new Mock<IMissaoRepository>();
        _service = new SistemaConcessaoService(
            _estudanteRepoMock.Object,
            _badgeRepoMock.Object,
            _missaoRepoMock.Object);
    }

    [Fact]
    public async Task ConcederBadgePorMissao_ComDadosValidos_DeveConcederBadge()
    {
        // Arrange
        var estudanteId = Guid.NewGuid();
        var missaoId = Guid.NewGuid();
        var badgeId = Guid.NewGuid();
        
        var estudante = new Estudante("João", "joao@email.com");
        var missao = new Missao("Missão Difícil", "Desafio complexo", 5, badgeId);
        var badge = new Badge("Expert", "Badge de expert", 5);
        
        // Simular que a missão foi concluída
        var missaoConcluida = new MissaoConcluida(missaoId, DateTime.UtcNow);
        estudante.GetType().GetField("_missoesConcluidas", 
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.SetValue(estudante, new List<MissaoConcluida> { missaoConcluida });

        _estudanteRepoMock.Setup(x => x.ObterComBadgesEMissoesAsync(estudanteId))
            .ReturnsAsync(estudante);
        _missaoRepoMock.Setup(x => x.ObterPorIdAsync(missaoId))
            .ReturnsAsync(missao);
        _badgeRepoMock.Setup(x => x.ObterPorIdAsync(badgeId))
            .ReturnsAsync(badge);

        // Act
        await _service.ConcederBadgePorMissaoAsync(estudanteId, missaoId);

        // Assert
        Assert.Single(estudante.Badges);
        Assert.Equal("Expert", estudante.Badges.First().Nome);
        _estudanteRepoMock.Verify(x => x.AtualizarAsync(estudante), Times.Once);
    }

    [Fact]
    public async Task ConcederBadgePorMissao_EstudanteNaoConcluiuMissao_DeveLancarExcecao()
    {
        // Arrange
        var estudanteId = Guid.NewGuid();
        var missaoId = Guid.NewGuid();
        var badgeId = Guid.NewGuid();
        
        var estudante = new Estudante("Maria", "maria@email.com");
        var missao = new Missao("Missão", "Descrição", 1, badgeId);

        _estudanteRepoMock.Setup(x => x.ObterComBadgesEMissoesAsync(estudanteId))
            .ReturnsAsync(estudante);
        _missaoRepoMock.Setup(x => x.ObterPorIdAsync(missaoId))
            .ReturnsAsync(missao);

        // Act & Assert
        await Assert.ThrowsAsync<SistemaConcessaoException>(() =>
            _service.ConcederBadgePorMissaoAsync(estudanteId, missaoId));
    }

    [Fact]
    public async Task ObterPontuacaoTotal_EstudanteExistente_DeveRetornarPontuacao()
    {
        // Arrange
        var estudanteId = Guid.NewGuid();
        var estudante = new Estudante("Carlos", "carlos@email.com");
        var badge = new Badge("Iniciante", "Badge inicial", 1);
        estudante.AdicionarBadge(badge);

        _estudanteRepoMock.Setup(x => x.ObterPorIdAsync(estudanteId))
            .ReturnsAsync(estudante);

        // Act
        var pontuacao = await _service.ObterPontuacaoTotalAsync(estudanteId);

        // Assert
        Assert.Equal(100, pontuacao);
    }
}