using API.Model.Entities;

namespace API.Service
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
