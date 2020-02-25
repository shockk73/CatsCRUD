using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CatsCRUD.Models;
using CatsCRUD.Services;
using CatsCRUD.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatsCRUD.Controllers
{
    [Route("api/[controller]")]
    public class CatsController : Controller
    {
        private readonly ICatService _catService;

        private readonly IMapper _mapper;

        public CatsController(ICatService catService, IMapper mapper)
        {
            _catService = catService;

            _mapper = mapper;
        }

    
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CatResponse>>> Get()
        {
            return Ok( _mapper.Map<IEnumerable<Cat>, IEnumerable<CatResponse>>((await _catService.GetAllAsync())));
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<CatResponse>> Get(int id)
        {
            var cat = await _catService.GetAsync(id);

            if (cat == null)
                return NotFound();


            return Ok(_mapper.Map<Cat, CatResponse>(cat));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<CatResponse>> Post([FromBody]CatRequest catReq)
        {
            if (catReq == null || !ModelState.IsValid)
                return BadRequest();

            await _catService.AddAsync(_mapper.Map<CatRequest, Cat>(catReq));
            
            return Ok();
        }

        [Authorize(Roles = "CatOwner")]
        [HttpPut]
        public async Task<ActionResult<CatResponse>> Put([FromBody]CatRequest catReq)
        {
            if (catReq == null || !ModelState.IsValid)
            {
                return BadRequest();
            }


            await _catService.UpdateAsync(_mapper.Map<CatRequest, Cat>(catReq));


            return Ok();
        }

        [Authorize(Roles = "CatOwner")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<CatResponse>> Delete(int id)
        {
            try
            {
                await _catService.DeleteAsync(id);
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
