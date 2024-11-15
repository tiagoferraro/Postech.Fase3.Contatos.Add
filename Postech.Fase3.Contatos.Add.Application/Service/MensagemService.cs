using System.Text.Json;
using Postech.Fase3.Contatos.Add.Application.DTO;
using Postech.Fase3.Contatos.Add.Application.Interface;
using Postech.Fase3.Contatos.Add.Domain.Entities;
using Postech.Fase3.Contatos.Add.Infra.CrossCuting;
using Postech.Fase3.Contatos.Add.Infra.CrossCuting.Model;

namespace Postech.Fase3.Contatos.Add.Application.Service;

public class MensagemService(IContatoService _contatoservice) : IMessageProcessor
{
    public async Task<ServiceResult<bool>> ProcessMessageAsync(string message)
    {
        try
        {
            var contatoDTO = JsonSerializer.Deserialize<ContatoDto>(message);
            return await _contatoservice.AdicionarAsync(new Contato(contatoDTO.ContatoId.Value, contatoDTO.Nome, contatoDTO.Telefone, contatoDTO.Email, contatoDTO.DddId, contatoDTO.DataInclusao));

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new ServiceResult<bool>(e);
        }
    }
}