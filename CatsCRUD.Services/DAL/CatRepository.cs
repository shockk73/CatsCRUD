using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CatsCRUD.Services.DAL
{
    public class CatRepository<T> : ICatRepository<T> where T : class
    {

        private readonly CatsContext _context;

        private readonly DbSet<T> _set;

        public CatRepository(CatsContext context)
        {
            _context = context;

            _set = context.Set<T>();
        }


        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = _set;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await _set.FindAsync(id);
        }

        public async Task InsertAsync(T cat)
        {
            await _set.AddAsync(cat);
        }

        public async Task DeleteAsync(int id)
        {
            var cat = await _set.FindAsync(id);

            _set.Remove(cat);
        }

        public async Task UpdateAsync(T cat)
        {
            _set.Update(cat);
        }


    }
}
