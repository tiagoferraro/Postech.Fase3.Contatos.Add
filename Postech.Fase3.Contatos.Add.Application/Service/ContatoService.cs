using AutoMapper;
using Postech.Fase3.Contatos.Add.Application.DTO;
using Postech.Fase3.Contatos.Add.Application.Interface;
using Postech.Fase3.Contatos.Add.Domain.Entities;
using Postech.Fase3.Contatos.Add.Infra.CrossCuting.Model;
using Postech.Fase3.Contatos.Add.Infra.Interface;

namespace Postech.Fase3.Contatos.Add.Application.Service;

public class ContatoService(IContatoRepository _contatoRepository,IMapper _mapper):IContatoService
{
    public async Task<ServiceResult<ContatoDTO>> AdicionarAsync(ContatoDTO? c)
    {
        try
        {
            var contato = _mapper.Map<Contato>(c);

            if (await _contatoRepository.Existe(contato))
                return new ServiceResult<ContatoDTO>(new ValidacaoException("Cadastro de contato ja existe"));

            var novoContato = await _contatoRepository.Adicionar(contato);

            return new ServiceResult<ContatoDTO>(_mapper.Map<ContatoDTO>(novoContato));
        }
        catch (Exception ex)
        {
            return new ServiceResult<ContatoDTO>(ex);
        }
    }
}