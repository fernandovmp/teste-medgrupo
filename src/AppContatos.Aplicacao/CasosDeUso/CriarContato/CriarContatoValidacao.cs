using AppContatos.Aplicacao.Services;
using FluentValidation;
using FluentValidation.Results;

namespace AppContatos.Aplicacao.CasosDeUso.CriarContato
{
    public class CriarContatoValidacao : AbstractValidator<CriarContatoInput>
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        public CriarContatoValidacao(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório")
                .MaximumLength(32).WithMessage("Nome deve ter no máximo 64 caractéres");
            RuleFor(x => x.DataNascimento)
                .Custom(ValidarIdade);
        }

        private void ValidarIdade(DateTime data, ValidationContext<CriarContatoInput> context)
        {
            if(data.Date.AddYears(18) > _dateTimeProvider.Now())
            {
                context
                    .AddFailure(
                        new ValidationFailure(
                            nameof(CriarContatoInput.DataNascimento),
                            "Contato deve ter mais de 18 anos"));
            }
        }
    }
}