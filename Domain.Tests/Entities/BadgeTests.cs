using Domain.Entities;
using Domain.Exceptions;

namespace Domain.Tests.Entities;

public class BadgeTests
{
    [Fact]
    public void CriarBadge_ComDadosValidos_DeveCriarBadge()
    {
        // Arrange
        var nome = "Primeira Missão";
        var descricao = "Concluiu a primeira missão com sucesso";
        var dificuldade = 2;
        
        // Act
        var badge = new Badge(nome, descricao, dificuldade);
        
        // Assert
        Assert.Equal(nome, badge.Nome);
        Assert.Equal(descricao, badge.Descricao);
        Assert.Equal(dificuldade, badge.Dificuldade);
        Assert.False(badge.Concedida);
        Assert.Null(badge.DataConcessao);
    }
    
    [Theory]
    [InlineData("", "Descrição válida", 1)]
    [InlineData(null, "Descrição válida", 1)]
    [InlineData("Nome válido", "", 1)]
    [InlineData("Nome válido", null, 1)]
    [InlineData("Nome válido", "Descrição válida", 0)]
    [InlineData("Nome válido", "Descrição válida", 6)]
    public void CriarBadge_ComDadosInvalidos_DeveLancarExcecao(string nome, string descricao, int dificuldade)
    {
        // Act & Assert
        Assert.Throws<BadgeException>(() => new Badge(nome, descricao, dificuldade));
    }
    
    [Fact]
    public void ConcederBadge_DeveMarcarComoConcedida()
    {
        // Arrange
        var badge = new Badge("Badge Teste", "Descrição", 1);
        
        // Act
        badge.Conceder();
        
        // Assert
        Assert.True(badge.Concedida);
        Assert.NotNull(badge.DataConcessao);
    }
    
    [Fact]
    public void ConcederBadgeJaConcedida_DeveLancarExcecao()
    {
        // Arrange
        var badge = new Badge("Badge Teste", "Descrição", 1);
        badge.Conceder();
        
        // Act & Assert
        Assert.Throws<BadgeException>(() => badge.Conceder());
    }
}