using System.Threading.Tasks;
using CatsCRUD.Services.Entites;

namespace CatsCRUD.Services
{
    public interface IAuthService
    {
        Task<User> AuthAsync(string username, string password);
    }
}
