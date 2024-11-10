namespace Postech.Fase3.Contatos.Add.Domain.Entities;

public class Contato
{
    public Guid ContatoId { get; private set; }
    public string Nome { get; private set; }
    public string Telefone { get; private set; }
    public string Email { get; private set; }
    public int DddId { get; private set; }
    public bool Ativo { get; private set; }
    public DateTime DataInclusao { get; private set; }

    public Contato(Guid contatoId, string nome, string telefone, string email, int dddId,DateTime dataInclusao)
    {
        ContatoId = contatoId;
        Nome = nome;
        Telefone = telefone;
        Email = email;
        DddId = dddId;
        DataInclusao = dataInclusao;
        Ativo = true;
    }

}