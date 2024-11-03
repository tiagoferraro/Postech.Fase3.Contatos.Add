namespace Postech.Fase3.Contatos.Add.Infra.CrossCuting.Model;

public class ValidacaoException : Exception
{
    public ValidacaoException(string mensagem) : base(mensagem)
    {
        
    }
}
