using FluentValidation.Results;
using TechLibrary.Api.Domain.Entities;
using TechLibrary.Api.Infraestructure.DataAccess;
using TechLibrary.Api.Infraestructure.Secutiry.Cryptography;
using TechLibrary.Api.Infraestructure.Secutiry.Tokens.Access;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Exception;

namespace TechLibrary.Api.UseCases.Users.Register;

public class RegisterUserUseCase
{
    public ResponseRegisteredUserJson Execute(RequestUserJson request)
    {
        var dbContext = new TechLibraryDbContext();

        Validate(request, dbContext);

        var cryptography = new BCryptAlgorithm();
        var entity = new User
        {
            Email = request.Email,
            Name = request.Name,
            Password = cryptography.HashPassword(request.Password),
        };

        dbContext.Users.Add(entity);
        dbContext.SaveChanges();

        var tokenGenerator = new JwtTokenGenerator();

        return new ResponseRegisteredUserJson
        {
            Name = entity.Name,
            AccessToken = tokenGenerator.Generate(entity)
        };
    }

    private void Validate(RequestUserJson request, TechLibraryDbContext dbContext)
    {
        var validator = new RegisterUserValidator();

        var result = validator.Validate(request);

        var existUserWithEmail = dbContext.Users.Any(user => user.Email.Equals(request.Email));
        if (existUserWithEmail)
            result.Errors.Add(new ValidationFailure("Email", "E-mail já registrado na plataforma!"));

        if (result.IsValid == false) 
        {
               var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
