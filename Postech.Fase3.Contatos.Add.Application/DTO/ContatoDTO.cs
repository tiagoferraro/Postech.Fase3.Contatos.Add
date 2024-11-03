namespace Postech.Fase3.Contatos.Add.Application.DTO;

public record ContatoDTO
{
    public int? ContatoId { get; init; }
    public string Nome { get; init; }
    public string Telefone { get; init; }
    public string Email { get; init; }
    public bool Ativo { get; init; }
    public int DddId { get; init; }
}