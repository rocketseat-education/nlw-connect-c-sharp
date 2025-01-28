using FluentValidation;
using TechLibrary.Communication.Requests;

namespace TechLibrary.Api.UseCases.Users.Register;

public class RegisterUserValidator : AbstractValidator<RequestUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(request => request.Name).NotEmpty().WithMessage("O nome é obrigatório.");
        RuleFor(request => request.Email).EmailAddress().WithMessage("O e-mail não é valido.");
        RuleFor(request => request.Password).NotEmpty().WithMessage("A senha é obrigatória.");
        When(request => string.IsNullOrEmpty(request.Password) == false, () =>
        {
            RuleFor(request => request.Password.Length).GreaterThanOrEqualTo(6).WithMessage("A senha deve ter mais que 6 caracteres.");
        });
    }
}
