using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MazeLib;
using Newtonsoft.Json.Linq;

namespace MazeSolverAPI.Controllers {
    [Route("api/[controller]")]
    public class SolverController : Controller
    {
        // POST api/solver
        [HttpPost]
        public List<Coordinates> Post()
        {
            string body = new System.IO.StreamReader(this.Request.Body).ReadToEnd();

            JObject data = JObject.Parse(body);

            Maze maze = data["maze"].ToObject<Maze>();

            Stack<Coordinates> path = new Stack<Coordinates>();

            IList<JToken> pathValues = data["path"].Children().ToList();
            foreach (JToken coord in pathValues) {
                Coordinates coordinates = coord.ToObject<Coordinates>();
                path.Push(coordinates);
            }

            Solver solver = new Solver(maze, path);

            return solver.Solve();
        }
    }
}
