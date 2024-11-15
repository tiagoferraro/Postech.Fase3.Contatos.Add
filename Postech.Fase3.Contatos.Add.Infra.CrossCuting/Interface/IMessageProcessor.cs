using Postech.Fase3.Contatos.Add.Infra.CrossCuting.Model;

namespace Postech.Fase3.Contatos.Add.Infra.CrossCuting;

public interface IMessageProcessor
{
    Task<ServiceResult<bool>> ProcessMessageAsync(string message);
}