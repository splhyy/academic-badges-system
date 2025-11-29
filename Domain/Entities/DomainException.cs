namespace Domain.Exceptions;

public abstract class DomainException : Exception
{
    protected DomainException(string message) : base(message) { }
}

public class BadgeException : DomainException
{
    public BadgeException(string message) : base(message) { }
}

public class EstudanteException : DomainException
{
    public EstudanteException(string message) : base(message) { }
}

public class MissaoException : DomainException
{
    public MissaoException(string message) : base(message) { }
}

public class SistemaConcessaoException : DomainException
{
    public SistemaConcessaoException(string message) : base(message) { }
}