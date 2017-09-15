using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MazeLib   {
    public class Maze {
        private int width;
        private int height;
        private MazeCell[,] cells;

        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }
        public MazeCell[,] Cells { get => cells; }

        public Maze(int width, int height) {
            this.width = width;
            this.height = height;
            this.cells = new MazeCell[height, width];
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    cells[x, y] = new MazeCell(x, y);
                }
            }
        }

        public MazeCell At(Coordinates coordinates) {
            return cells[coordinates.Row, coordinates.Col];
        }

        [JsonConstructor]
        public Maze(int width, int height, List<List<MazeCell>> cells) {
            this.width = width;
            this.height = height;

            int matHeight = cells.Count;
            int matWidth = cells[0].Count;

            if (!cells.TrueForAll(columns => columns.Count == matWidth)) {
                throw new ArgumentException($"Inconsistent column widths - expected {width}");
            } else if (width != matWidth || height != matHeight) {
                throw new ArgumentException($"Expected a cell matrix of {height}x{width}, got {matHeight}x{matWidth}");
            }

            this.cells = new MazeCell[width, height];
            for (int row = 0; row < height; row++) {
                for (int col = 0; col < width; col++) {
                    this.cells[row, col] = cells[row][col];
                }
            }
        }

        public static Maze Generate(int width, int height) {
            int seed = new Random().Next();
            return Generate(width, height, seed);
        }

        public static Maze Generate(int width, int height, int seed) {
            Maze maze = new Maze(width, height);

            Random rand = new Random(seed);

            Coordinates current = new Coordinates(0, 0);

            bool[,] visited = new Boolean[width, height];

            Stack<Coordinates> history = new Stack<Coordinates>();

            // The stack stores visited cells - start with 0, 0
            history.Push(current);

            while (history.Count > 0) {
                // Mark the current location as visited
                //Debug.WriteLine($"#MazeAPI Currently at [{row}, {col}]");
                visited[current.Row, current.Col] = true;

                List<String> validDirections = new List<String>();

                if (current.Col > 0 && !visited[current.Row, current.Col - 1]) {
                    validDirections.Add("Left");
                }
                if (current.Row > 0 && !visited[current.Row - 1, current.Col]) {
                    validDirections.Add("Up");
                }
                if (current.Col < width - 1 && !visited[current.Row, current.Col + 1]) {
                    validDirections.Add("Right");
                }
                if (current.Row < height - 1 && !visited[current.Row + 1, current.Col]) {
                    validDirections.Add("Down");
                }
                
                // If there are valid directions in which we can move
                if (validDirections.Count > 0) {
                    history.Push(current);

                    // Randomly select a movement direction from the valid list
                    int moveIndex = rand.Next(validDirections.Count);
                    string move = validDirections[moveIndex];

                    //Debug.WriteLine($"#MazeAPI Moving {move}");

                    switch (move) {
                    case "Left": {
                            maze.At(current).OpenLeft();
                            current = new Coordinates(current.Row, current.Col - 1);
                            maze.At(current).OpenRight();
                            break;
                        }
                    case "Up": {
                            maze.At(current).OpenUp();
                            current = new Coordinates(current.Row - 1, current.Col);
                            maze.At(current).OpenDown();
                            break;
                        }
                    case "Right": {
                            maze.At(current).OpenRight();
                            current = new Coordinates(current.Row, current.Col + 1);
                            maze.At(current).OpenLeft();
                            break;
                        }
                    case "Down": {
                            maze.At(current).OpenDown();
                            current = new Coordinates(current.Row + 1, current.Col);
                            maze.At(current).OpenUp();
                            break;
                        }
                    }
                } else {
                    // No valid moves, move back one step in history
                    current = history.Pop();
                }
            }

            // Open up the top-left cell as the starting point
            MazeCell start = maze.cells[0, 0];
            start.OpenUp();
            start.MakeStart();

            maze.Cells[width - 1, height - 1].MakeGoal();
            start.OpenDown();

            return maze;
        }
    }

    public struct Coordinates {
        public int Row;
        public int Col;

        public Coordinates (int row, int col) {
            this.Row = row;
            this.Col = col;
        }

        public override bool Equals(object obj) {
            if (!(obj is Coordinates))
                return false;

            Coordinates other= (Coordinates)obj;
            return this.Row == other.Row && this.Col == other.Col;

        }

        public override int GetHashCode() {
            var hashCode = 1084646500;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + Row.GetHashCode();
            hashCode = hashCode * -1521134295 + Col.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Coordinates coordinates1, Coordinates coordinates2) {
            return coordinates1.Equals(coordinates2);
        }

        public static bool operator !=(Coordinates coordinates1, Coordinates coordinates2) {
            return !(coordinates1 == coordinates2);
        }
    }

    public class MazeCell {

        public bool start { get; private set; }
        public bool goal { get; private set; }

        private Coordinates coordinates;

        public int row { get => coordinates.Row; private set => coordinates.Row = value; }
        public int col { get => coordinates.Col; private set => coordinates.Col = value; }
        public Coordinates Coordinates { get => coordinates; }

        // A true value for any of these represents a valid movement direction from this cell
        public bool up { get; private set; }
        public bool right { get; private set; }
        public bool down { get; private set; }
        public bool left { get; private set; }

        public MazeCell(int row,
                        int col) {
            this.coordinates = new Coordinates(row, col);
        }

        [JsonConstructor]
        public MazeCell(int row, int col, bool start, bool goal, bool up, bool right, bool down, bool left) {
            this.coordinates = new Coordinates(row, col);
            this.start = start;
            this.goal = goal;
            this.up = up;
            this.right = right;
            this.down = down;
            this.left = left;
        }

        public void MakeStart() {
            start = true;
        }

        public void MakeGoal() {
            goal = true;
        }

        public void OpenUp() {
            up = true;
        }

        public void OpenRight() {
            right = true;
        }

        public void OpenDown() {
            down = true;
        }

        public void OpenLeft() {
            left = true;
        }
    }
}
