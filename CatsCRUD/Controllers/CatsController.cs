using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatsCRUD.Models;
using CatsCRUD.Services;
using CatsCRUD.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatsCRUD.Controllers
{
    [Route("api/[controller]")]
    public class CatsController : Controller
    {
        CatService catService;
        public CatsController(CatService _catService)
        {
            catService = _catService;
        }

        [HttpGet]
        public IEnumerable<CatResponse> Get()
        {
            return catService.GetAll().Select( cat => new CatResponse { Id = cat.Id, Age = cat.Age, Name = cat.Name } );
        }

        
        [HttpGet("{id}")]
        public ActionResult<CatResponse> Get(int id)
        {
            var cat = catService.Get(id);

            if (cat == null)
                return NotFound();


            return Ok(cat);
        }


        [HttpPost]
        public ActionResult<CatResponse> Post(CatRequest catReq)
        {
            if (catReq == null || !ModelState.IsValid)
                return BadRequest();

            var cat = catService.Add(new Cat { Id = 0, Age = catReq.Age, Name = catReq.Name });
            
            return Ok(cat);
        }

       
        [HttpPut]
        public ActionResult<CatResponse> Put(CatRequest catReq)
        {
            if (catReq == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var cat = catService.Update(new Cat { Id = catReq.Id, Age = catReq.Age, Name = catReq.Name });

            if (cat == null)
                return NotFound();

            return Ok(cat);
        }

       
        [HttpDelete("{id}")]
        public ActionResult<CatResponse> Delete(int id)
        {
            var cat = catService.Delete(id);

            if (cat == null)
            {
                return NotFound();
            }
 
            return Ok(cat);
        }
    }
}
