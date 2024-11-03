using Postech.Fase3.Contatos.Add.Application.DTO;
using Postech.Fase3.Contatos.Add.Infra.CrossCuting.Model;

namespace Postech.Fase3.Contatos.Add.Application.Interface;

public interface IContatoService
{
    Task<ServiceResult<ContatoDTO>> AdicionarAsync(ContatoDTO? c);
}