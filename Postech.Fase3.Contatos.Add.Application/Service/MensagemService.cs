using System.Text.Json;
using Postech.Fase3.Contatos.Add.Application.DTO;
using Postech.Fase3.Contatos.Add.Application.Interface;
using Postech.Fase3.Contatos.Add.Infra.CrossCuting;

namespace Postech.Fase3.Contatos.Add.Application.Service;

public class MensagemService(IContatoService _contatoservice) : IMessageProcessor
{
    public async Task ProcessMessageAsync(string message)
    {
        try
        {
            await _contatoservice.AdicionarAsync(JsonSerializer.Deserialize<ContatoDTO>(message));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}