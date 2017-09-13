using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MazeAPI.Models;

namespace MazeAPI.Controllers
{
    [Route("api/[controller]")]
    public class MazeController : Controller
    {
        // GET api/maze/<size>
        [HttpGet("{size}")]
        public Maze Get(int size, string seedStr)
        {
            int seed;
            if (seedStr != null) {
                int.TryParse(seedStr, out seed);
            } else {
                seed = new Random().Next();
            }

            Maze maze = Maze.Generate(seed, size, size);
            return maze;
        }
    }
}
