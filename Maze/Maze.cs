using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MazeLib   {

    struct Move {
        public Coordinates coordinates;
        public string direction;

        public Move(Coordinates coordinates, string direction) {
            this.coordinates = coordinates;
            this.direction = direction;
        }
    }

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
        
        private List<Move> GetPossibleMoves(Coordinates current, Boolean[,] visited) {

            Move[] moves = new Move[4] {
                new Move(new Coordinates(current.Row, current.Col - 1), "left"),
                new Move(new Coordinates(current.Row - 1, current.Col), "up"),
                new Move(new Coordinates(current.Row, current.Col + 1), "right"),
                new Move(new Coordinates(current.Row + 1, current.Col), "down")
            };

            return moves
                .Where(move => move.coordinates.Row >= 0 && 
                               move.coordinates.Col >= 0 && 
                               move.coordinates.Row < height && 
                               move.coordinates.Col < width &&
                               !visited[move.coordinates.Row, move.coordinates.Col])
                .ToList();
        }

        public static Maze Generate(int width, int height, int seed) {
            Maze maze = new Maze(width, height);

            Random rand = new Random(seed);

            Coordinates current = new Coordinates(0, 0);
            maze.At(current).MakeStart();

            bool[,] visited = new Boolean[width, height];

            Stack<Coordinates> history = new Stack<Coordinates>();

            // The stack stores visited cells - start with 0, 0
            history.Push(current);

            while (history.Count > 0) {
                // Mark the current location as visited
                //Debug.WriteLine($"#MazeAPI Currently at [{row}, {col}]");
                visited[current.Row, current.Col] = true;

                List<Move> possibleMoves = maze.GetPossibleMoves(current, visited);

                // If there are valid directions in which we can move
                if (possibleMoves.Count > 0) {
                    history.Push(current);

                    // Randomly select a movement direction from the valid list
                    int moveIndex = rand.Next(possibleMoves.Count);
                    Move next = possibleMoves[moveIndex];

                    //Debug.WriteLine($"#MazeAPI Moving {move}");

                    switch (next.direction) {
                    case "left": {
                            maze.At(current).OpenLeft();
                            maze.At(next.coordinates).OpenRight();
                            break;
                        }
                    case "up": {
                            maze.At(current).OpenUp();
                            maze.At(next.coordinates).OpenDown();
                            break;
                        }
                    case "right": {
                            maze.At(current).OpenRight();
                            maze.At(next.coordinates).OpenLeft();
                            break;
                        }
                    case "down": {
                            maze.At(current).OpenDown();
                            maze.At(next.coordinates).OpenUp();
                            break;
                        }
                    }

                    current = next.coordinates;
                } else {
                    // No valid moves, move back one step in history
                    current = history.Pop();
                }
            }

            // Mark the bottom-right cell as the goal
            maze.Cells[width - 1, height - 1].MakeGoal();
            
            return maze;
        }
    }
}
