using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatsCRUD.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace CatsCRUD.Services
{
    public class CatService
    {

        readonly CatsContext _db;

        public CatService(CatsContext context)
        {
            _db = context;
        }

        public async Task AddAsync(Cat cat)
        {
            await _db.Cats.AddAsync(cat);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var cat = await _db.Cats.FirstOrDefaultAsync(x => x.Id == id);

            _db.Cats.Remove(cat);

            await _db.SaveChangesAsync();
        }

        public async Task<Cat> GetAsync(int id)
        {
            var cat = await _db.Cats.FirstOrDefaultAsync(c => c.Id == id);

            return cat;
        }

        public async Task<IEnumerable<Cat>> GetAllAsync()
        {
            return await _db.Cats.ToListAsync();
        }

        public async Task UpdateAsync(Cat cat)
        {
            _db.Update(cat);
            await _db.SaveChangesAsync();
        }
    }
}
