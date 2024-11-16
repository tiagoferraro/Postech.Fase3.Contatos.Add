namespace Postech.Fase3.Contatos.Add.Application.DTO;

public record ContatoDto
{
    public Guid? ContatoId { get; init; }
    public string Nome { get; init; }
    public string Telefone { get; init; }
    public string Email { get; init; }
    public bool Ativo { get; init; }
    public int DddId { get; init; }
    public DateTime DataInclusao { get; init; }

    public ContatoDto(Guid? contatoId, string nome, string telefone, string email, bool ativo, int dddId, DateTime dataInclusao)
    {
        ContatoId = contatoId;
        Nome = nome;
        Telefone = telefone;
        Email = email;
        Ativo = ativo;
        DddId = dddId;
        DataInclusao = dataInclusao;
    }
}