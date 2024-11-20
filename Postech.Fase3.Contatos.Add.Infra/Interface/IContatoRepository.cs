using Postech.Fase3.Contatos.Add.Domain.Entities;

namespace Postech.Fase3.Contatos.Add.Infra.Interface;

public interface IContatoRepository
{
    Task<Contato> Adicionar(Contato c);
    Task<bool> ExisteAsync(Contato c);
}