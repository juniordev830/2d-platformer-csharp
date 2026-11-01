using System;

namespace PlatformerGame
{
    /// <summary>Axis-aligned rectangle for simple 2D collision checks.</summary>
    public readonly record struct Rect
    {
        public float X { get; }
        public float Y { get; }
        public float Width { get; }
        public float Height { get; }
        public float Right => X + Width;
        public float Bottom => Y + Height;

        public Rect(float x, float y, float width, float height)
        {
            if (!float.IsFinite(x) || !float.IsFinite(y) ||
                !float.IsFinite(width) || !float.IsFinite(height))
            {
                throw new ArgumentOutOfRangeException(nameof(width), "Rectangle values must be finite.");
            }

            if (width < 0f) throw new ArgumentOutOfRangeException(nameof(width), "Width cannot be negative.");
            if (height < 0f) throw new ArgumentOutOfRangeException(nameof(height), "Height cannot be negative.");
            if (!float.IsFinite(x + width) || !float.IsFinite(y + height))
            {
                throw new ArgumentOutOfRangeException(nameof(width), "Rectangle bounds must remain finite.");
            }

            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        /// <summary>Returns true only when the rectangles overlap with positive area.</summary>
        public bool Intersects(in Rect other) =>
            X < other.Right &&
            Right > other.X &&
            Y < other.Bottom &&
            Bottom > other.Y;
    }
}
