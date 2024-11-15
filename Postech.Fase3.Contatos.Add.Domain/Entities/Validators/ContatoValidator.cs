using FluentValidation;

namespace Postech.Fase3.Contatos.Add.Domain.Entities.Validators;

public class ContatoValidator : AbstractValidator<Contato>
{
    public ContatoValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
            .WithMessage("Informe o nome do contato")
            .MaximumLength(150);

        RuleFor(x => x.Telefone)
            .NotEmpty()
            .WithMessage("Informe o telefone do cliente")
            .Must(x => 
            {
                var cleaned = x.Replace(" ", "").Replace("-", "");
                return int.TryParse(cleaned, out var val) && val > 9999999 && val <= 999999999;
            })
            .WithMessage("Informe o número do telefone de contato");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Informe o e-mail do cliente")
            .EmailAddress()
            .WithMessage("E-mail inválido");

        RuleFor(x => x.DddId)
            .NotEmpty()
            .InclusiveBetween(11, 99)
            .WithMessage("o código de área deve ser um inteiro de 2 dígitos.");
    }
}