
using Postech.Fase3.Contatos.Add.Domain.Entities;
using Postech.Fase3.Contatos.Add.Infra.Repository;
using Postech.Fase3.Contatos.Add.Infra.Repository.Context;
using Postech.Fase3.Contatos.Add.Integracao.Test.Fixture;
using Xunit.Extensions.Ordering;

namespace Postech.Fase3.Contatos.Add.Integracao.Test.Infra;

[Collection(nameof(ContextDbCollection)), Order(2)]
public class ContatoRepositoryTest
{
    private readonly AppDBContext context;
    private readonly ContatoRepository repository;
    public ContatoRepositoryTest(ContextDbFixture fixture)
    {
        context = fixture.Context!;
        repository = new ContatoRepository(context);
    }

    [Fact]
    public async Task Adicionar_DeveAdicionarContato()
    {
        var contato = new Contato(Guid.NewGuid() , "Nome 1", "999878587", "teste@email.com.br", 11,DateTime.Now);
        var result = await repository.Adicionar(contato);

        Assert.NotNull(result);
        Assert.Equal("Nome 1", result.Nome);
        Assert.Equal("999878587", result.Telefone);
        Assert.Equal(11, result.DddId);
    }

    [Fact]
    public async Task Existe_DeveRetornarTrueSeContatoExiste()
    {
        var contato = new Contato(Guid.NewGuid(), "Nome 1", "999878587", "teste@email.com.br", 11, DateTime.Now);
        context.Contatos.Add(contato);
        await context.SaveChangesAsync();

        var result = await repository.ExisteAsync(contato);
        
        Assert.True(result);
    }
}


