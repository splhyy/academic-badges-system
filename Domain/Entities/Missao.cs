using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Exceptions;

namespace Domain.Entities;

public class Missao
{
    public Guid Id { get; private set; }
    public string Titulo { get; private set; }
    public string Descricao { get; private set; }
    public int Dificuldade { get; private set; }
    public bool Concluida { get; private set; }
    public DateTime? DataConclusao { get; private set; }
    public Guid? BadgeRecompensaId { get; private set; }

    public Missao(string titulo, string descricao, int dificuldade = 1, Guid? badgeRecompensaId = null)
    {
        ValidarDados(titulo, descricao, dificuldade);
        
        Id = Guid.NewGuid();
        Titulo = titulo;
        Descricao = descricao;
        Dificuldade = dificuldade;
        Concluida = false;
        DataConclusao = null;
        BadgeRecompensaId = badgeRecompensaId;
    }

    public void Concluir()
    {
        if (Concluida)
            throw new MissaoException("Missão já foi concluída");
            
        Concluida = true;
        DataConclusao = DateTime.UtcNow;
    }

    public void AtribuirBadgeRecompensa(Guid badgeId)
    {
        BadgeRecompensaId = badgeId;
    }

    private void ValidarDados(string titulo, string descricao, int dificuldade)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            throw new MissaoException("Título da missão é obrigatório");
            
        if (string.IsNullOrWhiteSpace(descricao))
            throw new MissaoException("Descrição da missão é obrigatória");
            
        if (dificuldade < 1 || dificuldade > 5)
            throw new MissaoException("Dificuldade deve ser entre 1 e 5");
    }
}