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
            this.cells = new MazeCell[width, height];
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    cells[x, y] = new MazeCell(x, y);
                }
            }
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
                throw new ArgumentException($"Expected a cell matrix of {width}x{height}, got {matWidth}x{matHeight}");
            }

            this.cells = new MazeCell[width, height];
            for (int row = 0; row < height; row++) {
                for (int col = 0; col < width; col++) {
                    this.cells[row, col] = cells[row][col];
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

            Stack<Coordinates> history = new Stack<Coordinates>();

            // The stack stores visited cells - start with 0, 0
            history.Push(new Coordinates(row, col));

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
                    history.Push(new Coordinates(row, col));

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
                    Coordinates previous = history.Pop();
                    row = previous.Row;
                    col = previous.Col;
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

    public struct Coordinates {
        public int Row;
        public int Col;

        public Coordinates (int row, int col) {
            this.Row = row;
            this.Col = col;
        }
    }

    public class MazeCell {

        public bool start { get; private set; }
        public bool goal { get; private set; }

        private Coordinates coordinates;

        public int row { get => coordinates.Row; private set => coordinates.Row = value; }
        public int col { get => coordinates.Col; private set => coordinates.Col = value; }

        // A true value for any of these represents a valid movement direction from this cell
        public bool up { get; private set; }
        public bool right { get; private set; }
        public bool down { get; private set; }
        public bool left { get; private set; }

        public MazeCell(int row,
                        int col) {
            this.coordinates = new Coordinates(row, col);
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
