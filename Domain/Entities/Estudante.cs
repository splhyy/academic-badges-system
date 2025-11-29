using Domain.Exceptions;

namespace Domain.Entities;

public class Estudante
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public int PontuacaoTotal { get; private set; }
    public DateTime DataCadastro { get; private set; }
    
    private List<Badge> _badges;
    public IReadOnlyCollection<Badge> Badges => _badges.AsReadOnly();
    
    private List<MissaoConcluida> _missoesConcluidas;
    public IReadOnlyCollection<MissaoConcluida> MissoesConcluidas => _missoesConcluidas.AsReadOnly();

    public Estudante(string nome, string email)
    {
        ValidarDados(nome, email);
        
        Id = Guid.NewGuid();
        Nome = nome;
        Email = email;
        PontuacaoTotal = 0;
        DataCadastro = DateTime.UtcNow;
        _badges = new List<Badge>();
        _missoesConcluidas = new List<MissaoConcluida>();
    }

    public void AdicionarBadge(Badge badge)
    {
        if (_badges.Any(b => b.Id == badge.Id))
            throw new EstudanteException("Estudante já possui esta badge");
            
        badge.Conceder();
        _badges.Add(badge);
        PontuacaoTotal += CalcularPontuacaoBadge(badge);
    }

    public void ConcluirMissao(Missao missao)
    {
        if (_missoesConcluidas.Any(m => m.MissaoId == missao.Id))
            throw new EstudanteException("Missão já foi concluída por este estudante");
            
        missao.Concluir();
        _missoesConcluidas.Add(new MissaoConcluida(missao.Id, DateTime.UtcNow));
    }

    public bool PossuiBadge(Guid badgeId) => _badges.Any(b => b.Id == badgeId);
    public bool ConcluiuMissao(Guid missaoId) => _missoesConcluidas.Any(m => m.MissaoId == missaoId);

    private int CalcularPontuacaoBadge(Badge badge) => badge.Dificuldade * 100;

    private void ValidarDados(string nome, string email)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new EstudanteException("Nome do estudante é obrigatório");
            
        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            throw new EstudanteException("Email do estudante é inválido");
    }
}

public class MissaoConcluida
{
    public Guid MissaoId { get; private set; }
    public DateTime DataConclusao { get; private set; }

    public MissaoConcluida(Guid missaoId, DateTime dataConclusao)
    {
        MissaoId = missaoId;
        DataConclusao = dataConclusao;
    }
}