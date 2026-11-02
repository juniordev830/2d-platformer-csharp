using System;

namespace PlatformerGame
{
    /// <summary>Immutable 2D vector used by the platformer simulation.</summary>
    public readonly record struct Vector2D(float X, float Y)
    {
        public static Vector2D Zero => new(0f, 0f);

        public float Magnitude => (float)Math.Sqrt(((double)X * X) + ((double)Y * Y));

        public static Vector2D operator +(Vector2D left, Vector2D right) =>
            new(left.X + right.X, left.Y + right.Y);

        public static Vector2D operator -(Vector2D left, Vector2D right) =>
            new(left.X - right.X, left.Y - right.Y);

        public static Vector2D operator *(Vector2D vector, float scalar) =>
            new(vector.X * scalar, vector.Y * scalar);

        public Vector2D Normalize()
        {
            var magnitude = Math.Sqrt(((double)X * X) + ((double)Y * Y));
            return magnitude > 0d ? new Vector2D((float)(X / magnitude), (float)(Y / magnitude)) : Zero;
        }
    }
}
