using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class DinnerController : ApiController
    {
        [HttpGet("")]
        public IActionResult ListDinner() => Ok("empty list");
    }
}
