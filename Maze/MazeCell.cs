using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeLib
{
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
