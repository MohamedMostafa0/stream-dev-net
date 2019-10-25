using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MouseDev_Server_Api.Controllers
{
    [Route("project")]
    public class ProjectController : Controller
    {
        [HttpGet]
        [Route("a")]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}