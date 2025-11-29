using Domain.Entities;
using Domain.Exceptions;

namespace Domain.Tests.Entities;

public class EstudanteTests
{
    [Fact]
    public void CriarEstudante_ComDadosValidos_DeveCriarEstudante()
    {
        // Arrange
        var nome = "João Silva";
        var email = "joao@email.com";
        
        // Act
        var estudante = new Estudante(nome, email);
        
        // Assert
        Assert.Equal(nome, estudante.Nome);
        Assert.Equal(email, estudante.Email);
        Assert.Equal(0, estudante.PontuacaoTotal);
        Assert.Empty(estudante.Badges);
        Assert.Empty(estudante.MissoesConcluidas);
    }
    
    [Theory]
    [InlineData("", "joao@email.com")]
    [InlineData(null, "joao@email.com")]
    [InlineData("João Silva", "")]
    [InlineData("João Silva", null)]
    [InlineData("João Silva", "email-invalido")]
    public void CriarEstudante_ComDadosInvalidos_DeveLancarExcecao(string nome, string email)
    {
        // Act & Assert
        Assert.Throws<EstudanteException>(() => new Estudante(nome, email));
    }
    
    [Fact]
    public void AdicionarBadge_DeveAdicionarBadgeEAumentarPontuacao()
    {
        // Arrange
        var estudante = new Estudante("Maria", "maria@email.com");
        var badge = new Badge("Expert", "Badge de expert", 3);
        
        // Act
        estudante.AdicionarBadge(badge);
        
        // Assert
        Assert.Single(estudante.Badges);
        Assert.Equal(300, estudante.PontuacaoTotal);
        Assert.True(badge.Concedida);
    }
    
    [Fact]
    public void AdicionarBadgeDuplicada_DeveLancarExcecao()
    {
        // Arrange
        var estudante = new Estudante("Maria", "maria@email.com");
        var badge = new Badge("Expert", "Badge de expert", 3);
        estudante.AdicionarBadge(badge);
        
        // Act & Assert
        Assert.Throws<EstudanteException>(() => estudante.AdicionarBadge(badge));
    }
    
    [Fact]
    public void ConcluirMissao_DeveAdicionarMissaoConcluida()
    {
        // Arrange
        var estudante = new Estudante("Maria", "maria@email.com");
        var missao = new Missao("Missão 1", "Descrição", 2);
        
        // Act
        estudante.ConcluirMissao(missao);
        
        // Assert
        Assert.Single(estudante.MissoesConcluidas);
        Assert.True(missao.Concluida);
    }
}