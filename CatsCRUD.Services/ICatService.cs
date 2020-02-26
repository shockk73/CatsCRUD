using System.Collections.Generic;
using System.Threading.Tasks;
using CatsCRUD.Services.Models;

namespace CatsCRUD.Services
{
    public interface ICatService
    {
        Task AddAsync(Cat cat);

        Task DeleteAsync(int id);

        Task<Cat> GetAsync(int id);

        Task<IEnumerable<Cat>> GetAllAsync();

        Task UpdateAsync(Cat cat);
    }

}
