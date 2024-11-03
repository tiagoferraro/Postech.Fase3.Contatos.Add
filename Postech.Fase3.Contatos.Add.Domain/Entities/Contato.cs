namespace Postech.Fase3.Contatos.Add.Domain.Entities;

public class Contato
{
    public int? ContatoId { get; private set; }
    public string Nome { get; private set; }
    public string Telefone { get; private set; }
    public string Email { get; private set; }
    public int DddId { get; private set; }
    public DDD? Ddd { get; private set; }
    public bool Ativo { get; private set; }
    public DateTime DataInclusao { get; private set; }

    public Contato(int? contatoId, string nome, string telefone, string email, int dddId)
    {
        ContatoId = contatoId;
        Nome = nome;
        Telefone = telefone;
        Email = email;
        DddId = dddId;
        DataInclusao = DateTime.Now;
        Ativo = true;
    }

    public void DesativarContato()
    {
        Ativo = false;
    }

}