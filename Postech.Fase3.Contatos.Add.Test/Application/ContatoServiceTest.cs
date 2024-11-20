using Moq;
using Postech.Fase3.Contatos.Add.Application.Service;
using Postech.Fase3.Contatos.Add.Domain.Entities;
using Postech.Fase3.Contatos.Add.Infra.CrossCuting.Model;
using Postech.Fase3.Contatos.Add.Infra.Interface;

namespace Postech.Fase3.Contatos.Add.Test.Application;

public class ContatoServiceTest
{
    private readonly Contato _contato;
    private readonly Mock<IContatoRepository> contatoRepository;

    public ContatoServiceTest()
    {
        _contato = new Contato(Guid.NewGuid(), "nome Teste", "963333243", "teste@email.com.br", 11, DateTime.Now);

        contatoRepository = new Mock<IContatoRepository>();
    }

    [Fact]
    public async Task ContatoService_Adiconar_ComSucesso()
    {
        //arrange

        contatoRepository
            .Setup(x => x.Adicionar(_contato))
            .ReturnsAsync(_contato);


        var ContatoService =
            new ContatoService(contatoRepository.Object);

        //act
        var ContatoResult = await ContatoService.AdicionarAsync(_contato);

        //assert
        Assert.True(ContatoResult.IsSuccess);
        Assert.True(ContatoResult.Data);
    }

    [Fact]
    public async Task ContatoService_Adiconar_ComErroContatoJaExistente()
    {
        //arrange
        contatoRepository
            .Setup(x => x.ExisteAsync(It.IsAny<Contato>()))
            .ReturnsAsync(true);

        var contatoService = new ContatoService(contatoRepository.Object);

        //act
        var contatoResult = await contatoService.AdicionarAsync(_contato);

        //assert
        Assert.False(contatoResult.IsSuccess);
        var ex = Assert.IsType<ValidacaoException>(contatoResult.Error);
        Assert.Equal("Cadastro de contato ja existe", ex.Message);
    }

    [Fact]
    public async Task ContatoService_Adicionar_ComErro()
    {
        //arrange
        var contatoRepositoryError = new Mock<IContatoRepository>();
        contatoRepositoryError
            .Setup(x => x.Adicionar(It.IsAny<Contato>()))
            .ThrowsAsync(new Exception("Erro ao Adicionar"));
        var contatoService = new ContatoService(contatoRepositoryError.Object);

        //act
        var contatoResult = await contatoService.AdicionarAsync(_contato);

        //assert
        Assert.False(contatoResult.IsSuccess);
        Assert.IsType<Exception>(contatoResult.Error);
    }
}