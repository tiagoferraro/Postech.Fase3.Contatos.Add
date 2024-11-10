using Postech.Fase3.Contatos.Add.Domain.Entities;
using Postech.Fase3.Contatos.Add.Infra.CrossCuting.Model;

namespace Postech.Fase3.Contatos.Add.Application.Interface;

public interface IContatoService
{
    Task<ServiceResult<bool>> AdicionarAsync(Contato contato);
}