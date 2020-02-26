using System.Threading.Tasks;

namespace CatsCRUD.Services
{
    public interface IIdentityService
    {
        Task<string> TryCreateTokenAsync(string username, string password);
    }
}
