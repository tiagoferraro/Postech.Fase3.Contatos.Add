namespace Postech.Fase3.Contatos.Add.Infra.CrossCuting;

public interface IMessageProcessor
{
    Task ProcessMessageAsync(string message);
}