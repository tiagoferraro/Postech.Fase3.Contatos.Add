using System.Runtime.CompilerServices;
using Moq;
using Newtonsoft.Json;
using Postech.Fase3.Contatos.Add.Application.DTO;
using Postech.Fase3.Contatos.Add.Application.Interface;
using Postech.Fase3.Contatos.Add.Application.Service;
using Postech.Fase3.Contatos.Add.Domain.Entities;
using Postech.Fase3.Contatos.Add.Infra.CrossCuting.Model;
using Serilog;

namespace Postech.Fase3.Contatos.Add.Test.Application;

public class MensagemServiceTest
{
    private readonly ContatoDto contatoDto;
    private ILogger _logger;
    public MensagemServiceTest()
    {
        contatoDto = new ContatoDto(Guid.NewGuid(), "Nome teste", "963333243", "teste@gmail.com", true, 11, DateTime.Now);
        _logger = new Mock<ILogger>().Object;
    }
    [Fact]
    public async Task MensagemService_Processar_ComSucesso()
    {
        //arrange
        var contatoRepository = new Mock<IContatoService>();
        var mensagemService = new MensagemService(contatoRepository.Object, _logger);
        contatoRepository.Setup(x => x.AdicionarAsync(It.IsAny<Contato>()))
            .ReturnsAsync(new ServiceResult<bool>(true));

        //act
        var contatoResult = await mensagemService.ProcessMessageAsync(JsonConvert.SerializeObject(contatoDto));

        //assert
        Assert.True(contatoResult.IsSuccess);
    }
    [Fact]
    public async Task MensagemService_Processar_ComErro()
    {
        //arrange
        var contatoRepository = new Mock<IContatoService>();
        var mensagemService = new MensagemService(contatoRepository.Object, _logger);
        contatoRepository.Setup(x => x.AdicionarAsync(It.IsAny<Contato>()))
            .Throws(new Exception());

        //act
        var contatoResult = await mensagemService.ProcessMessageAsync(JsonConvert.SerializeObject(contatoDto));


        //assert
        Assert.True(!contatoResult.IsSuccess);
    }
}

