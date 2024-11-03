using Postech.Fase3.Contatos.Add.Domain.ObjectValue;

namespace Postech.Fase3.Contatos.Add.Domain.Entities;

public class DDD
{
    public int DddId { get; private set; }
    public UnidadeFederativa UnidadeFederativa { get; private set; }
    public string Regiao { get; private set; }


    /// <summary>
    /// EF constructor
    /// </summary>
    private DDD(int dddId,  string regiao)
    {
        DddId = dddId;
        Regiao = regiao;
    }

    /// <summary>
    /// Construtor para a entidade DDD
    /// </summary>
    /// <param name="dddId">inteiro com dois dígitos 10 e 99</param>
    /// <param name="siglaUf">Sigla da Unidade Federativa da regiao atendida, exemplos: MG,MT,AM,BA,RS</param>
    /// <param name="regiao">Regiao atendida, que pode compreender um conjunto de cidades, usualmente a principal cidade da regiao</param>
    public DDD(int dddId, string siglaUf, string regiao)
    {
        DddId = dddId;
        Regiao = regiao;
        UnidadeFederativa = new UnidadeFederativa(siglaUf);
    }
}
