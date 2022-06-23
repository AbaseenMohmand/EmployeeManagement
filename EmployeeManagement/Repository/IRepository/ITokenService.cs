using EmployeeManagement.Models;

namespace EmployeeManagement.Repository.IRepository
{
    public interface ITokenService
    {
        string BuildToken(string key, string issuer, ApplicationUser user);
        bool ValidateToken(string key, string issuer, string audience, string token);

    }
}
