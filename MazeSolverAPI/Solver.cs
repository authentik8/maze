using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MazeLib;

namespace MazeSolverAPI
{
    public class Solver {

        private Maze maze;
        private Stack<Coordinates> path;

       
        public Solver(Maze maze, Stack<Coordinates> path) {
            this.maze = maze;
            this.path = path;
        }

        public List<Coordinates> Solve() {

            Coordinates current;
            bool[,] visited = new Boolean[maze.Height, maze.Width];

            // Mark all co-ordinates already on the path as visited
            path.ToList().ForEach(coordinate => visited[coordinate.Row, coordinate.Col] = true);
            
            if (path.Count == 0) {
                path.Push(new Coordinates(0, 0));
            }

            current = path.Pop();
            
            
            while (!maze.At(current).goal) {
                // Mark the current location as visited
                visited[current.Row, current.Col] = true;

                List<Coordinates> possibleMoves = GetPossibleMoves(current, visited);

                if (possibleMoves.Count > 0) {
                    path.Push(current);
                    current = GetNextMove(possibleMoves);
                } else {
                    current = path.Pop();
                }
            }

            return path.Reverse().ToList();
        }

        private Coordinates GetNextMove(List<Coordinates> candidates) {
            int moveIndex = new Random().Next(candidates.Count);
            return candidates[moveIndex];
        }

        private List<Coordinates> GetPossibleMoves(Coordinates currentPosition, Boolean[,] visited) {
            List<Coordinates> possibles = new List<Coordinates>(4);

            // Pop & Re-Add `currentPosition` in order to retrieve the second item in the stack
            MazeCell current = maze.Cells[currentPosition.Row, currentPosition.Col];

            if (current.left) {
                possibles.Add(new Coordinates(current.row, current.col - 1));
            }

            if (current.up) {
                possibles.Add(new Coordinates(current.row - 1, current.col));
            }

            if (current.right) {
                possibles.Add(new Coordinates(current.row, current.col + 1));
            }

            if (current.down) {
                possibles.Add(new Coordinates(current.row + 1, current.col));
            }

            return possibles.Where(possible => !visited[possible.Row, possible.Col]).ToList();
        }
    }
}
