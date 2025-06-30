using FluentValidation;

namespace UniverSys.Application.Requests.Usuarios.Commands
{
    public class UsuarioRegistrarNovaSenhaCommandValidator : AbstractValidator<UsuarioRegistrarNovaSenhaCommand>
    {
        public UsuarioRegistrarNovaSenhaCommandValidator()
        {
            RuleFor(x => x.TokenRecuperacaoSenha)
                .NotEmpty().WithMessage("Token de recuperação não informado");

            RuleFor(x => x.NovaSenha)
                .MinimumLength(6).WithMessage("A senha deve ter ao menos 6 caracteres");

            RuleFor(x => x.NovaSenha)
                .Equal(y => y.ConfirmacaoNovaSenha).WithMessage("Confirmação de senha inválida");

        }
    }
}
