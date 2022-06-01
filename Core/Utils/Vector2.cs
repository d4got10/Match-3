namespace Match_3.Core.Utils
{
    public struct Vector2
    {

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float X { get; set; }
        public float Y { get; set; }


        public static readonly Vector2 Zero = new Vector2(0, 0);

        public static Vector2 Lerp(Vector2 from, Vector2 to, float t)
        {
            float x = from.X + (to.X - from.X) * t;
            float y = from.Y + (to.Y - from.Y) * t;

            return new Vector2(x, y);
        }

        public static Vector2 operator *(Vector2 vector, float value) => new Vector2(vector.X * value, vector.Y * value);
        public static Vector2 operator *(float value, Vector2 vector) => new Vector2(vector.X * value, vector.Y * value);
        public static Vector2 operator +(Vector2 first, Vector2 second) => new Vector2(second.X + first.X, second.Y + first.Y);
        public static Vector2 operator -(Vector2 first, Vector2 second) => new Vector2(second.X - first.X, second.Y - first.Y);
    }
}
