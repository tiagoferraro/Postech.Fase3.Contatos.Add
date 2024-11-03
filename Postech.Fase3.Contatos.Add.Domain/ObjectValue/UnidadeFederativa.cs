namespace Postech.Fase3.Contatos.Add.Domain.ObjectValue;

public class UnidadeFederativa
{
    public string Sigla { get; set; }
    public string Nome { get;  set; }

    private readonly Dictionary<string, string> _listaUfs = new()
    {
        {"AC","Acre"}, {"AL","Alagoas"}, {"AP","Amapá"}, {"AM","Amazonas"}, {"BA","Bahia"}, {"CE","Ceará"}, {"DF","Distrito Federal"}, {"ES","Espírito Santo"}, {"GO","Goiás"}, {"MA","Maranhão"}, {"MT","Mato Grosso"}, {"MS","Mato Grosso do Sul"}, {"MG","Minas Gerais"}, {"PA","Pará"}, {"PB","Paraíba"}, {"PR","Paraná"}, {"PE","Pernambuco"}, {"PI","Piauí"}, {"RJ","Rio de Janeiro"}, {"RN","Rio Grande do Norte"}, {"RS","Rio Grande do Sul"}, {"RO","Rondônia"}, {"RR","Roraima"}, {"SC","Santa Catarina"}, {"SP","São Paulo"}, {"SE","Sergipe"}, {"TO","Tocantins"}
    };


    public UnidadeFederativa(string sigla, string nome = "")
    {
        if (nome.Length > 0 || _listaUfs.TryGetValue(sigla, out nome!))
        {
            Sigla = sigla;
            Nome = nome;

        }
        else
            throw new InvalidOperationException("Sigla Não existe");

    }

}