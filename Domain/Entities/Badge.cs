 
using Domain.Exceptions;

namespace Domain.Entities;

public class Badge
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public bool Concedida { get; private set; }
    public DateTime? DataConcessao { get; private set; }
    public int Dificuldade { get; private set; }

    public Badge(string nome, string descricao, int dificuldade = 1)
    {
        ValidarDados(nome, descricao, dificuldade);
        
        Id = Guid.NewGuid();
        Nome = nome;
        Descricao = descricao;
        Dificuldade = dificuldade;
        Concedida = false;
        DataConcessao = null;
    }

    public void Conceder()
    {
        if (Concedida)
            throw new BadgeException("Badge já foi concedida");
            
        Concedida = true;
        DataConcessao = DateTime.UtcNow;
    }

    private void ValidarDados(string nome, string descricao, int dificuldade)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new BadgeException("Nome da badge é obrigatório");
            
        if (string.IsNullOrWhiteSpace(descricao))
            throw new BadgeException("Descrição da badge é obrigatória");
            
        if (dificuldade < 1 || dificuldade > 5)
            throw new BadgeException("Dificuldade deve ser entre 1 e 5");
    }
}