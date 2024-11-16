using FluentValidation;
using Postech.Fase3.Contatos.Add.Domain.Entities;
using Postech.Fase3.Contatos.Add.Domain.Entities.Validators;

namespace Postech.Fase3.Contatos.Add.Test.Domain;

public class ContatoTest
{
    [Fact]
    public void ContatoValidator_Contato_OK()
    {
        //arrange
        var guidContato = Guid.NewGuid();
        var dataInclusao = DateTime.Now;
        var contato = new Contato(guidContato, "João de Barro", "988808182", "Joao.Barro@acme.com", 27,dataInclusao
            );

        //act
        var contatoValidator = new ContatoValidator();
        var result = contatoValidator.Validate(contato);
        //assert
        Assert.True(result.IsValid);
        Assert.Equal(guidContato,contato.ContatoId);
        Assert.True(contato.Ativo);
        Assert.Equal(contato.DataInclusao,dataInclusao);
            
    }

    [Fact]
    public void ContatoValidator_NomeVazio_Error()
    {
        //aaa
        Assert.Throws<ValidationException>(() =>
            new Contato(Guid.NewGuid(), string.Empty, "988808182", "Joao.Barro@acme.com", 27, DateTime.Now));
    }

    [Theory]
    [InlineData("9 9788 8081")]
    [InlineData("9 9788-8081")]
    [InlineData("3327 6108")]
    [InlineData("33276108")]
    [InlineData("3327-6108")]
    public void ContatoValidator_Telefone_OK(string telefone)
    {
        //arrange
        var Contato = new Contato(Guid.NewGuid(), "João de Barro", telefone, "Joao.Barro@acme.com", 27, DateTime.Now);
        //act
        var contatoValidator = new ContatoValidator();
        var result = contatoValidator.Validate(Contato);
        //assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public void ContatoValidator_TelefoneEmpty_Error()
    {
        //aaa
        Assert.Throws<ValidationException>(() =>
            new Contato(Guid.NewGuid(), "João de Barro", "", "Joao.Barro@acme.com", 27, DateTime.Now));
    }

    [Fact]
    public void ContatoValidator_EmailEmpty_Error()
    {
        //aaa
        Assert.Throws<ValidationException>(() =>
            new Contato(Guid.NewGuid(), "João de Barro", "", string.Empty, 27, DateTime.Now));
    }

    [Fact]
    public void ContatoValidator_EmailInvalid_Error()
    {
        //aaa
        Assert.Throws<ValidationException>(() =>
            new Contato(Guid.NewGuid(), "João de Barro", "", "Joao.Barro", 27, DateTime.Now));
    }

    [Fact]
    public void ContatoValidator_DDDId_Invalid_Error()
    {
        //aaa
        Assert.Throws<ValidationException>(() =>
            new Contato(Guid.NewGuid(), "João de Barro", "", "Joao.Barro@acme.com", 127, DateTime.Now));
    }
}