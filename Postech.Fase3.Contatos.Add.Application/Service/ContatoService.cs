using Postech.Fase3.Contatos.Add.Application.Interface;
using Postech.Fase3.Contatos.Add.Domain.Entities;
using Postech.Fase3.Contatos.Add.Infra.CrossCuting.Model;
using Postech.Fase3.Contatos.Add.Infra.Interface;

namespace Postech.Fase3.Contatos.Add.Application.Service;

public class ContatoService(IContatoRepository _contatoRepository):IContatoService
{
    public async Task<ServiceResult<bool>> AdicionarAsync(Contato contato)
    {
        try
        {
            if (await _contatoRepository.ExisteAsync(contato))
                return new ServiceResult<bool>(new ValidacaoException("Cadastro de contato ja existe"));

            await _contatoRepository.Adicionar(contato);

            return new ServiceResult<bool>(true);
        }
        catch (Exception ex)
        {
            return new ServiceResult<bool>(ex);
        }
    }
}