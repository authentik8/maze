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
            this.cells = new MazeCell[width, height];
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    cells[x, y] = new MazeCell(x, y);
                }
            }
        }

        public static Maze Generate(int seed, int width, int height) {
            Maze maze = new Maze(width, height);

            //Debug.WriteLine($"#MazeAPI Using seed {seed}");
            Random rand = new Random(seed);

            int row = 0;
            int col = 0;

            bool[,] visited = new Boolean[width, height];

            Stack<Tuple<int, int>> history = new Stack<Tuple<int, int>>();

            // The stack stores visited cells - start with 0, 0
            history.Push(new Tuple<int, int>(0, 0));

            while (history.Count > 0) {
                // Mark the current location as visited
                //Debug.WriteLine($"#MazeAPI Currently at [{row}, {col}]");
                visited[row, col] = true;

                List<String> validDirections = new List<String>();

                if (col > 0 && !visited[row, col - 1]) {
                    validDirections.Add("Left");
                }
                if (row > 0 && !visited[row - 1, col]) {
                    validDirections.Add("Up");
                }
                if (col < width - 1 && !visited[row, col + 1]) {
                    validDirections.Add("Right");
                }
                if (row < height - 1 && !visited[row + 1, col]) {
                    validDirections.Add("Down");
                }

                //Debug.WriteLine($"#MazeAPI Valid Directions at [{row}, {col}]: [{string.Join(",", validDirections.ToArray())}]");

                // If there are valid directions in which we can move
                if (validDirections.Count > 0) {
                    history.Push(new Tuple<int, int>(row, col));

                    // Randomly select a movement direction from the valid list
                    int moveIndex = rand.Next(validDirections.Count);
                    string move = validDirections[moveIndex];

                    //Debug.WriteLine($"#MazeAPI Moving {move}");

                    switch (move) {
                    case "Left": {
                            maze.cells[row, col].OpenLeft();
                            col = col - 1;
                            maze.cells[row, col].OpenRight();
                            break;
                        }
                    case "Up": {
                            maze.cells[row, col].OpenUp();
                            row = row - 1;
                            maze.cells[row, col].OpenDown();
                            break;
                        }
                    case "Right": {
                            maze.cells[row, col].OpenRight();
                            col = col + 1;
                            maze.cells[row, col].OpenLeft();
                            break;
                        }
                    case "Down": {
                            maze.cells[row, col].OpenDown();
                            row = row + 1;
                            maze.cells[row, col].OpenUp();
                            break;
                        }
                    }
                } else {
                    // No valid moves, move back one step in history
                    var previous = history.Pop();
                    row = previous.Item1;
                    col = previous.Item2;
                    //Debug.WriteLine($"#MazeAPI Retracing steps to [{row}, {col}]");
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

    public class MazeCell {

        public bool start { get; private set; }
        public bool goal { get; private set; }

        public int row { get; private set; }
        public int col { get; private set; }

        // A true value for any of these represents a valid movement direction from this cell
        public bool up { get; private set; }
        public bool right { get; private set; }
        public bool down { get; private set; }
        public bool left { get; private set; }

        public MazeCell(int row,
                        int col) {
            this.row = row;
            this.col = col;
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
