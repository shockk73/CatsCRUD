using System;
using System.Collections.Generic;
using System.Linq;
using CatsCRUD.Models;
using CatsCRUD.Services.Models;

namespace CatsCRUD.Services
{
    public class CatService
    {

        CatsContext db;

        public CatService(CatsContext context)
        {
            db = context;
        }

        public Cat Add(Cat cat)
        {
            db.Cats.Add(cat);
            db.SaveChanges();

            return cat;
        }

        public Cat Delete(int id)
        {
            var cat = db.Cats.FirstOrDefault(x => x.Id == id);
            if (cat == null)
            {
                return null;
            }

            db.Cats.Remove(cat);
            db.SaveChanges();
            return cat;
        }

        public Cat Get(int id)
        {
            var cat = db.Cats.FirstOrDefault(c => c.Id == id);

            if (cat == null)
                return null;

            return cat;
        }

        public IEnumerable<Cat> GetAll()
        {
            return db.Cats.ToList();
        }

        public Cat Update(Cat cat)
        {
            if (!db.Cats.Any(x => x.Id == cat.Id))
            {
                return null;
            }

            db.Update(cat);
            db.SaveChanges();
            return cat;
        }
    }
}
