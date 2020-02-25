using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CatsCRUD.Services.Models;

namespace CatsCRUD.Services.DAL
{
    public interface ICatRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");

        Task<T> GetByIdAsync(object id);
        Task InsertAsync(T cat);
        Task DeleteAsync(int id);
        Task UpdateAsync(T cat);
    }
}
