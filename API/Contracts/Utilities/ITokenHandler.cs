using System.Security.Claims;

namespace API.Contracts.Utilities;

public interface ITokenHandler
{
    string GenerateToken(IEnumerable<Claim> claims);
}
