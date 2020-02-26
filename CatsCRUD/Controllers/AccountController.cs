using System.Threading.Tasks;
using CatsCRUD.Models;
using CatsCRUD.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatsCRUD.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IIdentityService _identityService;

        public AccountController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("token")]
        public async Task<ActionResult<TokenResponse>> Token([FromBody]TokenRequest tokenRequest)
        {

            var encToken = await _identityService.TryCreateTokenAsync(tokenRequest.Username, tokenRequest.Password);

            if (encToken == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            return Json(new TokenResponse{ Token = encToken, UserName = tokenRequest.Username});
        }

    }
}
