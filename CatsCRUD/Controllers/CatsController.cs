using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatsCRUD.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatsCRUD.Controllers
{
    [Route("api/[controller]")]
    public class CatsController : Controller
    {
        CatsContext db;
        public CatsController(CatsContext context)
        {
            db = context;
        }

        [HttpGet]
        public IEnumerable<Cat> Get()
        {
            return db.Cats.ToList();
        }

        
        [HttpGet("{id}")]
        public ActionResult<Cat> Get(int id)
        {
            var cat = db.Cats.FirstOrDefault(c => c.Id == id);

            if (cat == null)
                return NotFound();

            return cat;
        }


        [HttpPost]
        public ActionResult<Cat> Post(Cat cat)
        {
            if (cat == null)
                return BadRequest();

            db.Cats.Add(cat);
            db.SaveChanges();
            return Ok(cat);
        }

       
        [HttpPut("{id}")]
        public ActionResult<Cat> Put(int id, Cat cat)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (cat == null)
            {
                return BadRequest();
            }
            if (!db.Cats.Any(x => x.Id == id))
            {
                return NotFound();
            }

            db.Update(cat);
            db.SaveChanges();
            return Ok(cat);
        }

       
        [HttpDelete("{id}")]
        public ActionResult<Cat> Delete(int id)
        {
            var cat = db.Cats.FirstOrDefault(x => x.Id == id);
            if (cat == null)
            {
                return NotFound();
            }
            db.Cats.Remove(cat);
            db.SaveChanges();
            return Ok(cat);
        }
    }
}
