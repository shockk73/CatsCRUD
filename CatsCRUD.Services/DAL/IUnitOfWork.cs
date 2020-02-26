using System.Threading.Tasks;
using CatsCRUD.Services.Entites;
using CatsCRUD.Services.Models;

namespace CatsCRUD.Services.DAL
{

    public interface IUnitOfWork
    {
        ICatRepository<Cat> CatRepository { get; }

        ICatRepository<User> UserRepository { get; }

        Task SaveAsync();
    }
}
