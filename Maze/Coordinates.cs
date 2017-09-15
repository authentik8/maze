using System;
using System.Collections.Generic;
using System.Text;

namespace MazeLib
{
    public struct Coordinates {
        public int Row;
        public int Col;

        public Coordinates(int row, int col) {
            this.Row = row;
            this.Col = col;
        }

        public override string ToString() {
            return $"MazeLib.Coordinates({Row}, {Col})";
        }

        public override bool Equals(object obj) {
            if (!(obj is Coordinates))
                return false;

            Coordinates other = (Coordinates)obj;
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
}
