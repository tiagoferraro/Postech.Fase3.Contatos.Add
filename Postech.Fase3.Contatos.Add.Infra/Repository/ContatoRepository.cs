using Microsoft.EntityFrameworkCore;
using Postech.Fase3.Contatos.Add.Domain.Entities;
using Postech.Fase3.Contatos.Add.Infra.Interface;
using Postech.Fase3.Contatos.Add.Infra.Repository.Context;

namespace Postech.Fase3.Contatos.Add.Infra.Repository;

public class ContatoRepository(AppDBContext context) : IContatoRepository
{
    public async Task<Contato> Adicionar(Contato c)
    {
        context.Contatos.Add(c);
        await context.SaveChangesAsync();
        return c;
    }
    
    public async Task<bool> ExisteAsync(Contato c)
    {
        return await context.Contatos.AsNoTracking().AnyAsync(contato =>
            contato.Nome.Equals(c.Nome) && contato.Telefone.Equals(c.Telefone) && contato.DddId.Equals(c.DddId));
    }
}
