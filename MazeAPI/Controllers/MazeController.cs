using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MazeLib;
using System.Diagnostics;

namespace MazeAPI.Controllers
{
    [Route("api/[controller]")]
    public class MazeController : Controller
    {
        // GET api/maze/<size>
        [HttpGet("{size}")]
        public Maze Get(int size, [FromQuery]string seed)
        {
            int parsedSeed;

            Debug.WriteLine($"Seed: {seed}");

            if (seed != null) {
                int.TryParse(seed, out parsedSeed);
            } else {
                parsedSeed = new Random().Next();
            }

            Maze maze = Maze.Generate(size, size, parsedSeed);
            return maze;
        }
    }
}
