using TechLibrary.Api.Domain.Entities;

namespace TechLibrary.Api.Infraestructure.Secutiry.Cryptography;

public class BCryptAlgorithm
{
    public string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);

    public bool Verify(string password, User user) => BCrypt.Net.BCrypt.Verify(password, user.Password);
}
