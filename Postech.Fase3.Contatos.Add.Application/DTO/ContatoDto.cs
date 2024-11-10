namespace Postech.Fase3.Contatos.Add.Application.DTO;

public record ContatoDto(Guid? ContatoId,
    string Nome,
    string Telefone,
    string Email,
    bool Ativo,
    int DddId,
    DateTime DataInclusao);