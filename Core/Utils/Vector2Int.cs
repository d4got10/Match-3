using System;

namespace Match_3.Core.Utils
{
    public struct Vector2Int
    {
        public Vector2Int(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public static implicit operator Vector2(Vector2Int vector) => new Vector2(vector.X, vector.Y);
        public static Vector2Int operator *(Vector2Int vector, float value) => new Vector2Int((int)(vector.X * value), (int)(vector.Y * value));
        public static Vector2Int operator *(float value, Vector2Int vector) => new Vector2Int((int)(vector.X * value), (int)(vector.Y * value));
    }
}
