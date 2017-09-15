using Xunit;

namespace MazeLib.Tests {
    
    public class TestCoordinates {

        [Fact]
        public void ToString() {
            Coordinates coordinates = new Coordinates(53, 29);
            string stringValue = coordinates.ToString();
            Assert.Equal(stringValue, "MazeLib.Coordinates(53, 29)");
        }

        [Fact]
        public void Equals__True() {
            Coordinates first = new Coordinates(3, 4);
            Coordinates second = new Coordinates(3, 4);
            Assert.Equal(first, second);
            Assert.True(first == second);
        }

        [Fact]
        public void Equals__False() {
            Coordinates first = new Coordinates(2, 3);
            Coordinates second = new Coordinates(3, 2);
            Assert.NotEqual(first, second);
            Assert.True(first != second);
        }

    }
}
