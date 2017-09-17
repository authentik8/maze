using Xunit;
using MazeLibTests.Properties;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace MazeLib.Tests
{
    public class TestMaze
    {
        [Fact]
        public void TestAtAccessor() {
            Coordinates coordinates = new Coordinates(23, 7);
            Maze maze = new Maze(25, 25);

            Assert.Equal(maze.At(coordinates), maze.Cells[23, 7]);
        }

        [Fact]
        public void TestJsonConstructor() {
            string jsonMaze = Resources.FiveByFiveMazeJson;

            Maze maze = JObject.Parse(jsonMaze).ToObject<Maze>();

            Assert.Equal(maze.Width, 5);
            Assert.Equal(maze.Height, 5);

            Assert.Equal(maze.Cells.GetLength(0), 5);
            Assert.Equal(maze.Cells.GetLength(1), 5);

            Assert.True(maze.Cells[0, 0].start);
            Assert.True(maze.Cells[4, 4].goal);
        }

        [Fact]
        public void TestMazeGeneration() {
            Maze maze = Maze.Generate(6, 6, 12345);

            // Assert maze is generated and matches reference string
            string generatedJson = JsonConvert.SerializeObject(maze).ToLower();
            Assert.Equal(generatedJson, Resources.SixBySixMazeJson);
        }
    }
}
