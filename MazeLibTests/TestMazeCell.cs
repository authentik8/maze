using Xunit;
using Newtonsoft.Json.Linq;

namespace MazeLib.Tests
{
    public class TestMazeCell {

        [Fact]
        public void TestInitialization() {
            MazeCell cell = new MazeCell(23, 41);
            Assert.Equal(cell.Coordinates, new Coordinates(23, 41));
            Assert.Equal(cell.row, 23);
            Assert.Equal(cell.col, 41);
            Assert.False(cell.start);
            Assert.False(cell.goal);
            Assert.False(cell.left);
            Assert.False(cell.right);
            Assert.False(cell.up);
            Assert.False(cell.down);
        }

        [Fact]
        public void TestInitialization_FromJson() {
            string json = @"{""row"": 3, ""col"": 2, ""start"": false, ""goal"": false, ""up"": true, ""left"": true, ""right"": false, ""down"": false}";
            MazeCell cell = JObject.Parse(json).ToObject<MazeCell>();

            Assert.Equal(cell.row, 3);
            Assert.Equal(cell.col, 2);
            Assert.False(cell.start);
            Assert.False(cell.goal);
            Assert.True(cell.left);
            Assert.True(cell.up);
            Assert.False(cell.right);
            Assert.False(cell.down);
        }

        [Fact]
        public void TestMakeStart() {
            MazeCell cell = new MazeCell(0, 0);
            cell.MakeStart();
            Assert.True(cell.start);
        }

        [Fact]
        public void TestMakeGoal() {
            MazeCell cell = new MazeCell(15, 15);
            cell.MakeGoal();
            Assert.True(cell.goal);
        }

        [Fact]
        public void TestOpenUp() {
            MazeCell cell = new MazeCell(3, 2);
            cell.OpenUp();
            Assert.True(cell.up);
        }

        [Fact]
        public void TestOpenRight() {
            MazeCell cell = new MazeCell(3, 2);
            cell.OpenRight();
            Assert.True(cell.right);
        }

        [Fact]
        public void TestOpenDown() {
            MazeCell cell = new MazeCell(3, 2);
            cell.OpenLeft();
            Assert.True(cell.left);
        }

    }
}
