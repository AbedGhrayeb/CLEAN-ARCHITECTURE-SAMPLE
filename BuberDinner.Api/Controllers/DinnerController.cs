using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace BuberDinner.Api.Controllers
{
    [Route("[controller]")]
    public class DinnerController : ApiController
    {
        public IActionResult ListDinner() => Ok(Array.Empty<string>);
    }
}
