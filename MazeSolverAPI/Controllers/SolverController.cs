using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MazeSolverAPI.Controllers
{
    [Route("api/[controller]")]
    public class SolverController : Controller
    {
        // POST api/solver
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
    }
}
