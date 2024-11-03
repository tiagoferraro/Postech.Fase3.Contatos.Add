using AutoMapper;
using Postech.Fase3.Contatos.Add.Application.DTO;
using Postech.Fase3.Contatos.Add.Domain.Entities;

namespace Postech.Fase3.Contatos.Add.Application.Mapping;

public class ContatoMapingProfile : Profile
{
    public ContatoMapingProfile()
    {
        CreateMap<Contato, ContatoDTO>()
            .ConstructUsing(x => new ContatoDTO()
            {
                DddId = x.DddId,
                Nome = x.Nome,
                Ativo = x.Ativo,
                ContatoId = x.ContatoId,
                Email = x.Email,
                Telefone = x.Telefone
            });
        CreateMap<ContatoDTO, Contato>()
            .ConstructUsing(x =>
                new Contato(x.ContatoId,x.Nome,x.Telefone,x.Email,x.DddId  ));    
    }
    
}