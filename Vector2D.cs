using System;

namespace PlatformerGame
{
    /// <summary>Immutable 2D vector used by the platformer simulation.</summary>
    public readonly record struct Vector2D(float X, float Y)
    {
        public static Vector2D Zero => new(0f, 0f);

        public float Magnitude => MathF.Sqrt((X * X) + (Y * Y));

        public static Vector2D operator +(Vector2D left, Vector2D right) =>
            new(left.X + right.X, left.Y + right.Y);

        public static Vector2D operator -(Vector2D left, Vector2D right) =>
            new(left.X - right.X, left.Y - right.Y);

        public static Vector2D operator *(Vector2D vector, float scalar) =>
            new(vector.X * scalar, vector.Y * scalar);

        public Vector2D Normalize()
        {
            var magnitude = Magnitude;
            return magnitude > 0f ? new Vector2D(X / magnitude, Y / magnitude) : Zero;
        }
    }
}
