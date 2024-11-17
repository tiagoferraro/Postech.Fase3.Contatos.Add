using System.Text.Json;
using Postech.Fase3.Contatos.Add.Application.DTO;
using Postech.Fase3.Contatos.Add.Application.Interface;
using Postech.Fase3.Contatos.Add.Domain.Entities;
using Postech.Fase3.Contatos.Add.Infra.CrossCuting;
using Postech.Fase3.Contatos.Add.Infra.CrossCuting.Model;
using Serilog;

namespace Postech.Fase3.Contatos.Add.Application.Service;

public class MensagemService(IContatoService _contatoService,ILogger _logger) : IMessageProcessor
{
    public async Task<ServiceResult<bool>> ProcessMessageAsync(string message)
    {
        try
        {
            _logger.Information("Processing message: {Message}", message);
            var contatoDto = JsonSerializer.Deserialize<ContatoDto>(message);
            var result = await _contatoService.AdicionarAsync(new Contato(contatoDto!.ContatoId!.Value, contatoDto.Nome, contatoDto.Telefone, contatoDto.Email, contatoDto.DddId, contatoDto.DataInclusao));
            _logger.Information("Message processed successfully: {Message}", message);
            return result;

        }
        catch (Exception e)
        {
            _logger.Error(e, "Error processing message: {Message}", message);
            return new ServiceResult<bool>(e);
        }
    }
}